using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;
public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //// Create a request using a URL that can receive a post. 
        //WebRequest request = WebRequest.Create("http://rastreo3.estafeta.com/RastreoWebInternet/consultaEnvio.do");
        //// Set the Method property of the request to POST.
        //request.Method = "POST";
        //// Create POST data and convert it to a byte array.
        //string postData = "idioma=es&dispatch=doRastreoInternet&guias=0019999999703610019220&tipoGuia=ESTAFETA";
        //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //// Set the ContentType property of the WebRequest.
        //request.ContentType = "application/x-www-form-urlencoded";
        //// Set the ContentLength property of the WebRequest.
        //request.ContentLength = byteArray.Length;
        //// Get the request stream.
        //Stream dataStream = request.GetRequestStream();
        //// Write the data to the request stream.
        //dataStream.Write(byteArray, 0, byteArray.Length);
        //// Close the Stream object.
        //dataStream.Close();
        //// Get the response.
        //WebResponse response = request.GetResponse();
        //// Display the status.
        //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        //// Get the stream containing content returned by the server.
        //dataStream = response.GetResponseStream();
        //// Open the stream using a StreamReader for easy access.
        //StreamReader reader = new StreamReader(dataStream);
        //// Read the content.
        //string responseFromServer = reader.ReadToEnd().ToString();
        //// Display the content.
        //Console.WriteLine(responseFromServer);

        //HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        //htmlDoc.LoadHtml(responseFromServer);

        //var res = htmlDoc.DocumentNode.SelectNodes("//span[@class='style17']");
        //foreach (var item in res)
        //{
        //    string x = item.InnerHtml;
                      
        //}
        
        //lblGuia.Text = res[0].InnerHtml.ToString();
        //lblNumRastreo.Text = res[1].InnerHtml.ToString();
        //lblDetalle.Text = res[2].InnerHtml.ToString();

        //// Clean up the streams.
        //reader.Close();
        //dataStream.Close();
        //response.Close();
    }
}