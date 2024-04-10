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
                #region ruc.com.pe
                /* metodo con ruc.com.pe
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
                        string json = numero + "{\"token\":\"eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Imx1Y2lvLnNvbG9yemFub0BnbWFpbC5jb20ifQ.ZVIcx1EuIzl9xiSbUTYVRru4vJI-KpjXYauXhGsL_wk" + "\"}";
                        streamWriter.Write(json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var masticado = JObject.Parse(result);
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
                    string datos = "";
                    try
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = "{\"token\":\"f193780e-9239-464e-918d-548f96210a39-364baff6-543f-4cc7-a783-047d9c820d7e\"," +
                                          "\"dni\":\"" + numero + "\"}";
                            streamWriter.Write(json);
                        }
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
                */
                #endregion
                #region apisperu
                string numero = args[0].ToString();
                // create a request
                ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Imx1Y2lvLnNvbG9yemFub0BnbWFpbC5jb20ifQ.ZVIcx1EuIzl9xiSbUTYVRru4vJI-KpjXYauXhGsL_wk";
                if (args[1].ToString() == "0")
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dniruc.apisperu.com/api/v1/ruc/" + numero + "?token=" + token);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "GET";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        string datos = "";
                        var masticado = JObject.Parse(result);
                        if (masticado["success"].ToString().ToLower() == "false")
                        {
                            datos = "00000000000".ToString().Trim() + "|" + "RUC NO EXISTE O CON BAJA DE OFICIO";
                        }
                        else
                        {
                            if (masticado.HasValues == true)
                            {
                                datos = masticado["ruc"].ToString() + "|" +
                                    masticado["razonSocial"].ToString() + "|" +
                                    masticado["estado"].ToString() + "|" +
                                    masticado["condicion"].ToString() + "|" +
                                    masticado["ubigeo"].ToString() + "|" +
                                    masticado["direccion"].ToString() + "|" +
                                    masticado["distrito"].ToString() + "|" +
                                    masticado["provincia"].ToString() + "|" +
                                    masticado["departamento"].ToString();
                            }
                        }
                        Console.WriteLine(datos);
                        //Console.ReadKey();
                    }
                }
                else
                {   // args[1].ToString() == "1"
                    string datos = "";
                    try
                    {
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://dniruc.apisperu.com/api/v1/dni/" + numero + "?token=" + token);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "GET";
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            var masticado = JObject.Parse(result);
                            if (masticado.HasValues == true)
                            {
                                if (masticado["success"].ToString().ToLower() == "true")
                                {
                                    datos = masticado["dni"].ToString().Trim() + "|" +
                                        masticado["nombres"].ToString().Trim() + " " +
                                        masticado["apellidoPaterno"].ToString().Trim() + " " +
                                        masticado["apellidoMaterno"].ToString().Trim();
                                }
                                else
                                {
                                    datos = "00000000".ToString().Trim() + "|" + "CLIENTE NO IDENTIFICADO";
                                }
                            }
                            else
                            {
                                datos = "00000000".ToString().Trim() + "|" + "CLIENTE NO IDENTIFICADO";
                            }
                        }
                    }
                    catch
                    {
                        datos = "00000000".ToString().Trim() + "|" + "CLIENTE NO IDENTIFICADO";
                    }
                    Console.WriteLine(datos);
                    //Console.ReadKey();
                }
                #endregion
            }
        }
    }
}
