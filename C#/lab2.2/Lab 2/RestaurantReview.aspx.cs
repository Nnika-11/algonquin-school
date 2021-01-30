using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Serialization;


public partial class RestaurantReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        
        if (!IsPostBack)
        {
            //Use the names of the restaurants in the XML file  to populate the dropdown list

        }
    }

    protected void drpRestaurants_SelectedIndexChanged(object sender, EventArgs e)
    {

        //show the selected restaurant data as specified in the lab requirements 
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Save the changed restaurant restaurant data back to the XML file. 
    }


}