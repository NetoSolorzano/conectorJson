using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace conectorJson
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) Environment.Exit(0);
            if (args[0].ToString().Length < 8 || (args[1].ToString() != "0" && args[1].ToString() != "1"))
            {
                Environment.Exit(0);
            }
            else
            {
                string numero = args[0].ToString();
                // create a request
                ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/consultas");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                if (args[1].ToString() == "0")
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"token\":\"f193780e-9239-464e-918d-548f96210a39-364baff6-543f-4cc7-a783-047d9c820d7e\"," +
                                      "\"ruc\":\"" + numero + "\"}";
                        streamWriter.Write(json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var masticado = JObject.Parse(result);
                        /*
                        Console.WriteLine(masticado["success"]);
                        Console.WriteLine(masticado["ruc"]);
                        Console.WriteLine(masticado["nombre_o_razon_social"]);
                        Console.WriteLine(masticado["estado_del_contribuyente"]);
                        Console.WriteLine(masticado["condicion_de_domicilio"]);
                        Console.WriteLine(masticado["ubigeo"]);
                        Console.WriteLine(masticado["tipo_de_via"]);
                        Console.WriteLine(masticado["nombre_de_via"]);
                        Console.WriteLine(masticado["codigo_de_zona"]);
                        Console.WriteLine(masticado["tipo_de_zona"]);
                        Console.WriteLine(masticado["numero"]);
                        Console.WriteLine(masticado["interior"]);
                        Console.WriteLine(masticado["lote"]);
                        Console.WriteLine(masticado["dpto"]);
                        Console.WriteLine(masticado["manzana"]);
                        Console.WriteLine(masticado["kilometro"]);
                        Console.WriteLine(masticado["departamento"]);
                        Console.WriteLine(masticado["provincia"]);
                        Console.WriteLine(masticado["distrito"]);
                        Console.WriteLine(masticado["direccion"]);
                        Console.WriteLine(masticado["direccion_completa"]);
                        Console.WriteLine(masticado["ultima_actualizacion"]);
                        */
                        //Console.ReadKey();
                        string datos = "";
                        if (masticado["success"].ToString() == "True")
                        {
                            datos = masticado["ruc"].ToString() + "|" +
                                masticado["nombre_o_razon_social"].ToString() + "|" +
                                masticado["estado_del_contribuyente"].ToString() + "|" +
                                masticado["condicion_de_domicilio"].ToString() + "|" +
                                masticado["ubigeo"].ToString() + "|" +
                                masticado["direccion"].ToString() + "|" +
                                masticado["distrito"].ToString() + "|" +
                                masticado["provincia"].ToString() + "|" +
                                masticado["departamento"].ToString();
                        }
                        Console.WriteLine(datos);
                    }
                }
                else
                {   // args[1].ToString() == "1"
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"token\":\"f193780e-9239-464e-918d-548f96210a39-364baff6-543f-4cc7-a783-047d9c820d7e\"," +
                                      "\"dni\":\"" + numero + "\"}";
                        streamWriter.Write(json);
                    }
                    string datos = "";
                    try
                    {
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            var masticado = JObject.Parse(result);
                            //Console.WriteLine(masticado["success"]);
                            //Console.WriteLine(masticado["dni"]);
                            //Console.WriteLine(masticado["nombre_completo"]);
                            //Console.ReadKey();
                            if (masticado["success"].ToString() == "True")
                            {
                                datos = masticado["dni"].ToString().Trim() + "|" +
                                    masticado["nombre_completo"].ToString();
                            }
                        }
                    }
                    catch
                    {
                        datos = "00000000".ToString().Trim() + "|" + "CLIENTE NO IDENTIFICADO";
                    }
                    Console.WriteLine(datos);
                }
                //Console.ReadKey();
            }
        }
    }
}
