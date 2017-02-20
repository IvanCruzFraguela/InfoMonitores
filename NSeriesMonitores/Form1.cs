using IvanCruz.Util;
using IvanCruz.Util.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSeriesMonitores {
    public partial class Form1 : Form {
        public static class C {
            public const string NInventario = "NInventario";
            public const string Host = "Host";
            public const string Sala = "Sala";
            public const string Ubicacion = "Ubicacion";
            public const string MarcaMonitor = "MarcaMonitor";
            public const string ModeloMonitor = "ModeloMonitor";
            public const string NSMonitor = "NSMonitor";
            public const string UltimaHoraRespuestaPing = "UltimaHoraRespuestaPing";
            public const string DataFileName = "Datos.xml";
            public const string NSerieEntradaFileName = "numeros serie monitores a localizar.txt";
            public const string NSerieSalidaFileName = "numeros serie monitores localizados.txt";
            public const string GridFileName = "DatosGrid.txt";
            public const string RespondioPing = "RespondioPing";
            public const string Status = "Status";
        }
        public Form1() {
            InitializeComponent();
            InicializarComponentes();
        }
        DataTable Table;
        BindingSource Bs;
        CLogTextBox Log;
        private void InicializarComponentes() {
            this.RellenaDiccionarioCodMarcaMonitor();
            this.Log = new CLogTextBox(ELogLevel.All, this.tbMensajes);
            Table = new DataTable();
            Table.TableName = "DatosPCs";
            Table.Columns.Add(C.NInventario);
            Table.Columns.Add(C.Host);
            Table.Columns.Add(C.MarcaMonitor);
            Table.Columns.Add(C.ModeloMonitor);
            Table.Columns.Add(C.NSMonitor);
            Table.Columns.Add(C.Status);
            Table.Columns.Add(C.UltimaHoraRespuestaPing);
            Table.Columns.Add(C.RespondioPing);
            Table.Columns.Add(C.Sala);
            Table.Columns.Add(C.Ubicacion);
            Table.ReadXml(C.DataFileName);
            Bs = new BindingSource(Table, "");
            SUtilUI.DGVInit(this.dgv, Bs);
            this.dgv.MultiSelect = true;
            SUtilUI.AnadirColumna(this.dgv, C.NInventario, "Inventario", 80);
            SUtilUI.AnadirColumna(this.dgv, C.Host, "Host", 110);
            SUtilUI.AnadirColumna(this.dgv, C.MarcaMonitor, "Marca Monitor", 250);
            SUtilUI.AnadirColumna(this.dgv, C.ModeloMonitor, "Modelo Monitor", 250);
            SUtilUI.AnadirColumna(this.dgv, C.NSMonitor, "Nº Serie Monitor", 250);
            SUtilUI.AnadirColumna(this.dgv, C.Status, "Status", 150);
            SUtilUI.AnadirColumna(this.dgv, C.UltimaHoraRespuestaPing, "Last Ping", 150);
            SUtilUI.AnadirColumna(this.dgv, C.RespondioPing, "Ping", 110);
            SUtilUI.AnadirColumna(this.dgv, C.Sala, "Sala", 70);
            SUtilUI.AnadirColumna(this.dgv, C.Ubicacion, "Ubicación", 250);
            this.dgv.Font = new Font("Courier", 10);
        }
        private void btnCargarEquiposNuevos_Click(object sender, EventArgs e) {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK) {
                    int i = 0;
                    foreach (string linea in File.ReadAllLines(ofd.FileName, Encoding.Default)) {
                        i++;
                        if (!string.IsNullOrWhiteSpace(linea)) {
                            string aux = linea.Replace("\t", "");
                            string[] partes = aux.Split('#');
                            if (partes.Length < 3) {
                                MessageBox.Show("Error: en línea " + i.ToString());
                                break;
                            }
                            DataRow[] rowQueYaExiste = this.Table.Select(C.NInventario + "='" + partes[0] + "'");
                            if (rowQueYaExiste.Length > 0) {
                                RellenarDatosDeFichero(partes, rowQueYaExiste[0]);
                            } else {
                                DataRow row = this.Table.NewRow();
                                RellenarDatosDeFichero(partes, row);
                                this.Table.Rows.Add(row);
                            }
                            this.Log.Debug(rowQueYaExiste.Length.ToString());
                        }
                    }
                    this.Log.Info($"{Table.Rows.Count} equipos cargados");
                }
            }
        }
        private void RellenarDatosDeFichero(string[] partes, DataRow row) {
            row[C.NInventario] = partes[0];
            row[C.Host] = "hpchv" + partes[0].PadLeft(6, '0') + "p";
            row[C.Sala] = partes[1];
            row[C.Ubicacion] = this.Junta(partes, 2, " \\ ");
        }
        private string Junta(string[] partes, int start, string separador) {
            StringBuilder sb = new StringBuilder();
            bool primero = true;
            for (int i = start; i < partes.Length; i++) {
                if (primero) {
                    primero = false;
                } else {
                    sb.Append(separador);
                }
                sb.Append(partes[i]);
            }
            return sb.ToString();
        }
        private void btnComprobarNSerie_Click(object sender, EventArgs e) {
            int max = 30;
            int cantEquipos = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var rows = this.dgv.SelectedRows;
            this.progressBar.Maximum = rows.Count;
            this.progressBar.Value = 0;
            foreach (DataGridViewRow item in rows) {
                //if (cantEquipos > max) { return; }
                cantEquipos++;
                string nInventario = item.Cells[0].Value.ToString();
                DataRow row = SelectRowByNInventario(nInventario);
                string host = row[C.Host].ToString();
                bool respondioPing = Ping(host);
                if (respondioPing) {
                    row[C.UltimaHoraRespuestaPing] = DateTime.Now;
                    this.RellenarDatosMonitor(row);
                } else {
                    row[C.Status] = EStatus.NoPing;
                }
                row[C.RespondioPing] = respondioPing;
                this.Log.Info($"{host}; Ping={respondioPing}");
                this.progressBar.PerformStep();
            }
            sw.Stop();
            DateTime auxTime = new DateTime(sw.ElapsedTicks);
            this.Log.Info($"{cantEquipos} revisados en {auxTime.ToLongTimeString()}");
        }

        enum EStatus { OK, NoPing, ErrorDesconocido, ErrorClaseNoValida, ErrorComException, ErrorAccesoNoAutorizado }
        private void RellenarDatosMonitor(DataRow row) {
            EStatus status = EStatus.OK;
            string host = row[C.Host].ToString();
            ManagementScope scope = new ManagementScope($"\\\\{host}\\root\\wmi");
            ObjectQuery query = new ObjectQuery("SELECT * FROM WmiMonitorId");

            ManagementObjectSearcher mos = new ManagementObjectSearcher(scope, query);

            StringBuilder sbNumerosDeSerie = new StringBuilder();
            StringBuilder sbMarcas = new StringBuilder();
            StringBuilder sbModelos = new StringBuilder();
            bool primero = true;
            try {
                ManagementObjectCollection moc = mos.Get();
                foreach (ManagementObject mo in mos.Get()) {
                    if (primero) {
                        primero = false;
                    } else {
                        sbNumerosDeSerie.Append(",");
                        sbMarcas.Append(",");
                        sbModelos.Append(",");
                    }
                    string aux;
                    try {
                        aux = this.TraduceNSerie(mo.Properties["SerialNumberID"]);
                        sbNumerosDeSerie.Append(aux);
                    } catch (Exception ex) {
                        this.Log.Error(ex.Message);
                        this.Log.Error("Error en traducción de Número de Serie en equipo " + host);
                    }
                    try { 
                        aux = this.TraduceMarca(mo.Properties["ManufacturerName"]);
                        sbMarcas.Append(aux);
                    } catch (Exception ex) {
                        this.Log.Error(ex.Message);
                        this.Log.Error("Error en traducción de Fabricante en equipo " + host);
                    }
                    try { 
                        //aux = this.TraduceModelo(mo.Properties["ProductCodeID"]);
                        aux = this.TraduceModelo(mo.Properties["UserFriendlyName"]);
                        sbModelos.Append(aux);
                    } catch (Exception ex) {
                        this.Log.Error(ex.Message);
                        this.Log.Error("Error en traducción de Modelo en equipo " + host);
                    }
                }
                row[C.NSMonitor] = sbNumerosDeSerie.ToString();
                row[C.MarcaMonitor] = sbMarcas.ToString();
                row[C.ModeloMonitor] = sbModelos.ToString();
            } catch (ManagementException mex) {
                status = EStatus.ErrorDesconocido;
                if (mex.Message.Equals("Clase no válida")) {
                    status = EStatus.ErrorClaseNoValida;
                }
                this.LogException(host, mex.Message);
            } catch (COMException cex) {
                status = EStatus.ErrorComException;
                this.LogException(host, cex.Message);
            } catch (UnauthorizedAccessException uaex) {
                status = EStatus.ErrorAccesoNoAutorizado;
                this.LogException(host, uaex.Message);
            } catch (Exception ex) {
                status = EStatus.ErrorDesconocido;
                this.LogException(host, ex.Message);
            }
            row[C.Status] = status;
        }

        private string TraduceModelo(PropertyData propertyData) {
            StringBuilder sb = new StringBuilder();
            if ((propertyData.Value != null) &&(propertyData.Value is UInt16[])) {
                foreach (UInt16 item in (UInt16[])propertyData.Value) {
                    if (item != 0) {
                        byte NumeroEnByte = Convert.ToByte(item);
                        byte[] auxArray = new byte[1];
                        auxArray[0] = NumeroEnByte;
                        string aux = Encoding.ASCII.GetString(auxArray);
                        sb.Append(aux);
                    }
                }
            } else {
                if(propertyData.Value == null) {
                    throw new Exception("El modelo devuelve nulo");
                }
                throw new Exception("El tipo del valor del número de serie no es UInt16[]");
            }
            return sb.ToString();
        }

        private string TraduceMarca(PropertyData propertyData) {
            StringBuilder sb = new StringBuilder();
            if (propertyData.Value is UInt16[]) {
                foreach (UInt16 item in (UInt16[])propertyData.Value) {
                    if (item != 0) {
                        byte NumeroEnByte = Convert.ToByte(item);
                        byte[] auxArray = new byte[1];
                        auxArray[0] = NumeroEnByte;
                        string aux = Encoding.ASCII.GetString(auxArray);
                        sb.Append(aux);
                    }
                }
            } else {
                throw new Exception("El tipo del valor del número de serie no es UInt16[]");
            }
            if (this.DCodMarcaMonitor.ContainsKey(sb.ToString())){
                return this.DCodMarcaMonitor[sb.ToString()];
            }else { 
                return sb.ToString() + "(Marca desconocida)";
            }
        }

        private void LogException(string host, string message) {
            this.Log.Error("Error en: " + host + ". " + message);
        }

        private string TraduceNSerie(PropertyData propertyData) {
            StringBuilder sb = new StringBuilder();
            if (propertyData.Value is UInt16[]) {
                foreach (UInt16 item in (UInt16[])propertyData.Value) {
                    if (item != 0) {
                        byte NumeroEnByte = Convert.ToByte(item);
                        byte[] auxArray = new byte[1];
                        auxArray[0] = NumeroEnByte;
                        string aux = Encoding.ASCII.GetString(auxArray);
                        sb.Append(aux);
                    }
                }
            } else {
                throw new Exception("El tipo del valor del número de serie no es UInt16[]");
            }
            return sb.ToString();
        }

        private DataRow SelectRowByNInventario(string nInventario) {
            return this.Table.Select($"{C.NInventario} = '{nInventario}'")[0];
        }

        public bool Ping(string Pc) {
            Ping ping = new Ping();
            try {
                PingReply pr = ping.Send(Pc, 5 * 1000);//5 segundos
                if (pr.Status == IPStatus.Success) {
                    return true;
                }
            } catch (PingException) {
                return false;
            }
            return false;
        }
        private void btnGrabarDatos_Click(object sender, EventArgs e) {
            GrabarDatos();
        }

        private void GrabarDatos() {
            this.Table.WriteXml(C.DataFileName);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult respuesta = MessageBox.Show("Desea Grabar antes de salir", "Atención:", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (respuesta) {
                case DialogResult.Yes:
                    this.GrabarDatos();
                    break;
                case DialogResult.No:
                    //Nada que hacer
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default: throw new Exception("Caso no considerado");
            }
        }
        public class Monitor {
            public string NSerie;
            public string Host;
            public string Sala;
            public string Ubicacion;
            public Monitor(string nSerie) {
                this.NSerie = nSerie;
            }
        }


        private void btnCargarNSeriesMonitores_Click(object sender, EventArgs e) {
            List<Monitor> lSinEncontrar = new List<Monitor>();
            List<Monitor> lEncontrados = new List<Monitor>();
            foreach (string item in File.ReadAllLines(C.NSerieEntradaFileName)) {
                lSinEncontrar.Add(new Monitor(item.Trim()));
            }
            foreach (DataRow item in this.Table.Rows) {
                object aux = item[C.Status];
                if (aux != DBNull.Value) {
                    string TextoStatus = Convert.ToString(aux);
                    EStatus EstadoRow = (EStatus)Enum.Parse(typeof(EStatus), TextoStatus);
                    Monitor MonitorEncontrado;
                    if (EstadoRow == EStatus.OK) {
                        string[] NSerieDelRow = item[C.NSMonitor].ToString().Split(',');
                        for (int i = 0; i < NSerieDelRow.Length; i++) {
                            NSerieDelRow[i] = NSerieDelRow[i].Trim();
                        }
                        foreach (string nSerie in NSerieDelRow) {
                            if (nSerie.StartsWith(" ") || nSerie.EndsWith(" ")) {
                                throw new Exception("Espacio que no debería haber");
                            }
                            MonitorEncontrado = lSinEncontrar.Find(x => x.NSerie == nSerie);
                            if (MonitorEncontrado != null) {
                                MonitorEncontrado.Host = item[C.Host].ToString();
                                MonitorEncontrado.Sala = item[C.Sala].ToString();
                                MonitorEncontrado.Ubicacion = item[C.Ubicacion].ToString();
                                this.Log.Info($"Host: {item[C.Host]}, NSerie: {nSerie}");
                                lEncontrados.Add(MonitorEncontrado);
                                lSinEncontrar.Remove(MonitorEncontrado);
                            }
                        }
                    }
                }
            }
            this.GrabarFicheroMonitoresEncontrados(lEncontrados);
            this.Log.Info("Encontrados: " + lEncontrados.Count.ToString());
        }
        private void GrabarFicheroMonitoresEncontrados(List<Monitor> lEncontrados) {
            using (FileStream fs = File.Open(C.NSerieSalidaFileName, FileMode.Create)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.WriteLine("NSerie#Host#Sala#Ubicacion");
                    foreach (Monitor item in lEncontrados) {
                        sw.WriteLine($"{item.NSerie}#{item.Host}#{item.Sala}#{item.Ubicacion}");
                    }
                }
            }
        }
        private void dgv_SelectionChanged(object sender, EventArgs e) {
            this.labelSeleccionados.Text = "Seleccionados: " + this.dgv.SelectedRows.Count.ToString();
        }
        private void btnGrabarGrid_Click(object sender, EventArgs e) {
            using (FileStream fs = File.OpenWrite(C.GridFileName)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    int cantidad = 0;
                    foreach (DataGridViewRow item in this.dgv.SelectedRows) {
                        bool primero = true;
                        foreach (DataGridViewTextBoxCell cell in item.Cells) {
                            if (primero) {
                                primero = false;
                            } else {
                                sw.Write("#");
                            }
                            sw.Write(cell.Value.ToString());
                        }
                        sw.WriteLine();
                        cantidad++;
                    }
                    MessageBox.Show($"Grabadas {cantidad} filas en {C.GridFileName}");
                }
            }
        }
        Dictionary<string, string> DCodMarcaMonitor = new Dictionary<string, string>();
        private void RellenaDiccionarioCodMarcaMonitor() {
            this.DCodMarcaMonitor.Add("AAC", "AcerView");
            this.DCodMarcaMonitor.Add("ACR", "Acer");
            this.DCodMarcaMonitor.Add("AOC", "AOC");
            this.DCodMarcaMonitor.Add("AIC", "AG Neovo");
            this.DCodMarcaMonitor.Add("APP", "Apple Computer");
            this.DCodMarcaMonitor.Add("AST", "AST Research");
            this.DCodMarcaMonitor.Add("AUO", "Asus");
            this.DCodMarcaMonitor.Add("BNQ", "BenQ");
            this.DCodMarcaMonitor.Add("CMO", "Acer");
            this.DCodMarcaMonitor.Add("CPL", "Compal");
            this.DCodMarcaMonitor.Add("CPQ", "Compaq");
            this.DCodMarcaMonitor.Add("CPT", "Chunghwa Pciture Tubes, Ltd.");
            this.DCodMarcaMonitor.Add("CTX", "CTX");
            this.DCodMarcaMonitor.Add("DEC", "DEC");
            this.DCodMarcaMonitor.Add("DEL", "Dell");
            this.DCodMarcaMonitor.Add("DPC", "Delta");
            this.DCodMarcaMonitor.Add("DWE", "Daewoo");
            this.DCodMarcaMonitor.Add("EIZ", "EIZO");
            this.DCodMarcaMonitor.Add("ELS", "ELSA");
            this.DCodMarcaMonitor.Add("ENC", "EIZO");
            this.DCodMarcaMonitor.Add("EPI", "Envision");
            this.DCodMarcaMonitor.Add("FCM", "Funai");
            this.DCodMarcaMonitor.Add("FUJ", "Fujitsu");
            this.DCodMarcaMonitor.Add("FUS", "Fujitsu-Siemens");
            this.DCodMarcaMonitor.Add("GSM", "LG Electronics");
            this.DCodMarcaMonitor.Add("GWY", "Gateway 2000");
            this.DCodMarcaMonitor.Add("HEI", "Hyundai");
            this.DCodMarcaMonitor.Add("HIT", "Hyundai");
            this.DCodMarcaMonitor.Add("HSL", "Hansol");
            this.DCodMarcaMonitor.Add("HTC", "Hitachi/Nissei");
            this.DCodMarcaMonitor.Add("HWP", "HP");
            this.DCodMarcaMonitor.Add("IBM", "IBM");
            this.DCodMarcaMonitor.Add("ICL", "Fujitsu ICL");
            this.DCodMarcaMonitor.Add("IVM", "Iiyama");
            this.DCodMarcaMonitor.Add("KDS", "Korea Data Systems");
            this.DCodMarcaMonitor.Add("LEN", "Lenovo");
            this.DCodMarcaMonitor.Add("LGD", "Asus");
            this.DCodMarcaMonitor.Add("LPL", "Fujitsu");
            this.DCodMarcaMonitor.Add("MAX", "Belinea");
            this.DCodMarcaMonitor.Add("MEI", "Panasonic");
            this.DCodMarcaMonitor.Add("MEL", "Mitsubishi Electronics");
            this.DCodMarcaMonitor.Add("MS_", "Panasonic");
            this.DCodMarcaMonitor.Add("NAN", "Nanao");
            this.DCodMarcaMonitor.Add("NEC", "NEC");
            this.DCodMarcaMonitor.Add("NOK", "Nokia Data");
            this.DCodMarcaMonitor.Add("NVD", "Fujitsu");
            this.DCodMarcaMonitor.Add("OPT", "Optoma");
            this.DCodMarcaMonitor.Add("PHL", "Philips");
            this.DCodMarcaMonitor.Add("REL", "Relisys");
            this.DCodMarcaMonitor.Add("SAN", "Samsung");
            this.DCodMarcaMonitor.Add("SAM", "Samsung");
            this.DCodMarcaMonitor.Add("SBI", "Smarttech");
            this.DCodMarcaMonitor.Add("SGI", "SGI");
            this.DCodMarcaMonitor.Add("SNY", "Sony");
            this.DCodMarcaMonitor.Add("SRC", "Shamrock");
            this.DCodMarcaMonitor.Add("SUN", "Sun Microsystems");
            this.DCodMarcaMonitor.Add("SEC", "Hewlett-Packard");
            this.DCodMarcaMonitor.Add("TAT", "Tatung");
            this.DCodMarcaMonitor.Add("TOS", "Toshiba");
            this.DCodMarcaMonitor.Add("TSB", "Toshiba");
            this.DCodMarcaMonitor.Add("VSC", "ViewSonic");
            this.DCodMarcaMonitor.Add("ZCM", "Zenith");
            this.DCodMarcaMonitor.Add("UNK", "Unknown");
            this.DCodMarcaMonitor.Add("_YV", "Fujitsu");
        }

        private void btnInforme_Click(object sender, EventArgs e) {
            StringBuilder sb = new StringBuilder();
            //Total de equipos.
            sb.AppendLine($"Equipos en los que se busca: {this.Table.Rows.Count}");
            //Total de números de serie
            sb.AppendLine($"Números de serie encontrados: {this.ContarNumerosDeSerie()}");
            //Total por state
            sb.AppendLine("Totales por estado:");
            Dictionary<EStatus, int> dCantEstados = new Dictionary<EStatus, int>();
            foreach (DataRow dr in this.Table.Rows) {
                string aux = dr[C.Status].ToString();
                EStatus eaux = (EStatus)Enum.Parse(typeof(EStatus), aux);
                if (dCantEstados.ContainsKey(eaux)) {
                    dCantEstados[eaux] += 1;
                } else {
                    dCantEstados[eaux] = 1;
                }
            }
            foreach (var item in EStatus.GetValues(typeof(EStatus))) {
                sb.Append(item.ToString());
                sb.Append(" = ");
                sb.AppendLine(dCantEstados[(EStatus)item].ToString());
            }
            FMemo.Show(sb.ToString());
        }

        private int ContarNumerosDeSerie() {
            int result = 0;
            foreach (DataRow row in this.Table.Rows) {
                string[] aux = row[C.NSMonitor].ToString().Split(',');
                foreach (string num in aux) {
                    if (!string.IsNullOrWhiteSpace(num)) {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
