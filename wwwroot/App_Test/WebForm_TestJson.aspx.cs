using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wwwroot.App_Test
{
    public partial class WebForm_TestJson : System.Web.UI.Page
    {
        protected void Test(object sender, EventArgs e)
        {
            #region //1.JsonReader应用
            //string jsonText = @"{""input"":""value"", ""output"":""result""}";
            //JsonReader reader = new JsonTextReader(new StringReader(jsonText));

            //while (reader.Read())
            //{
            //    Response.Write("<br/>"+reader.TokenType + "|" + reader.ValueType + "|" + reader.Value);
            //}
            #endregion

            #region //2.JsonWriter使用
            //StringWriter sw = new StringWriter();
            //JsonWriter writer = new JsonTextWriter(sw);

            //writer.WriteStartObject();
            //writer.WritePropertyName("input");
            //writer.WriteValue("value");
            //writer.WritePropertyName("output");
            //writer.WriteValue("result");
            //writer.WriteEndObject();
            //writer.Flush();

            //string jsonText = sw.GetStringBuilder().ToString();
            //Response.Write(jsonText);
            #endregion

            #region //3.JObject,JProperty,JArray使用
            //var jo = new JObject(new JProperty("age", 21), new JProperty("funny", true), new JProperty("array", new JArray(new[] { 2, 4, 1 })));
            //jo.Add("name", "孙战平");
            //var jc=new JArray(new JObject(new JProperty("name","szh")));
            //jo.Add("childs", jc);
            //Response.Write(jo.ToString());
            #endregion

            #region //4.JavaScriptSerializer使用
            //Project p = new Project() { Input = "stone", Output = "gold" };
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(p);
            //Response.Write(json);

            //var p1 = serializer.Deserialize<Project>(json);
            //Response.Write("<br/>"+p1.Input + "=>" + p1.Output);
            //Response.Write("<br/>"+ReferenceEquals(p, p1));
            #endregion

            #region //5.Json.JsonToObject
            //Project p = Json.GetJsonObject<Project>("{\"Input\":\"stone\",\"Output\":\"gold\"}",false);
            //Response.Write(p.Input + "=>" + p.Output);/

            //Project p = new Project() { Input = "stone", Output = "gold" };
            //Response.Write(Json.GetJsonString(p,false));

            //ProjectS p = Json.GetJsonObject<ProjectS>("{\"Input\":\"stone\",\"Output\":\"gold\"}",true);
            //Response.Write(p.Input + "=>" + p.Output);

            //ProjectS p = new ProjectS() { Input = "stone", Output = "gold" };
            //Response.Write(Json.GetJsonString(p,true));

            //Project p = new Project() { Input = "stone", Output = "gold" };
            //String s = JsonConvert.SerializeObject(p, Formatting.Indented);
            //Response.Write(s);
            #endregion
            Response.Write(new WX.Json.BuildUserMenus(100).GetUserMenu());
        }
    }
    class Project
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }
    [DataContract]
    class ProjectS
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string Input { get; set; }
        [DataMember(Order = 2)]
        public string Output { get; set; }
    }
    [DataContract]
    class Person
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string Name { get; set; }
        [DataMember(Order = 1)]
        public int Age { get; set; }
        [DataMember(Order = 2)]
        public bool Alive { get; set; }
        [DataMember(Order = 3)]
        public string[] FavoriteFilms { get; set; }
        [DataMember(Order = 4)]
        public Person Child { get; set; }
    }
    public class Json
    {
        public static T GetJsonObject<T>(string jsonString, bool objectIsSerialized)
        {
            if (objectIsSerialized)
            {
                using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
                { return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms); }
            }
            else
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                try
                {
                    return jss.Deserialize<T>(jsonString);
                }
                catch
                {
                    return default(T);
                }
            }
        }
        public static string GetJsonString(object jsonObject, bool objectIsSerialized)
        {
            if (objectIsSerialized)
            {
                using (var ms = new MemoryStream())
                {
                    new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                    return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            else
            {
                return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //try
                //{
                //    return jss.Serialize(jsonObject);
                //}
                //catch
                //{
                //    return String.Empty;
                //}
            }
        }
    }

}