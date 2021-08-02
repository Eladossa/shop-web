using DAL;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace BLL
{
    public class BLL
    {

        public static string AddProduct(string productName, string categoryName, double price,
            int stock, string productDescription,
                         string productOverview, string productImage)
        {
            bool addProduct = DAL.AddProduct(productName, categoryName, price,
             stock, productDescription,
                          productOverview, productImage);
            return new JavaScriptSerializer().Serialize(addProduct);
        }

        public static string ProductList()

        {
            DataTable productsTable = DAL.GetProducts();
            return DataTableToJson(productsTable);


        }



        public static string DataTableToJson(DataTable table)
        {
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                              + "\":" + "\""
                                              + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                              + "\":" + "\""
                                              + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        jsonString.Append("}");
                    }
                    else
                    {
                        jsonString.Append("},");
                    }
                }
                jsonString.Append("]");
            }
            return jsonString.ToString();
        }
    }
}
