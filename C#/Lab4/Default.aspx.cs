using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Click += new EventHandler(delegate (Object o, EventArgs a)
        {
            Response.Redirect("Default.aspx");
        });
        BulletedList menulist = (BulletedList)Master.FindControl("topMenu");
        if (!IsPostBack)
        {
            menulist.Items.Add(new ListItem("Add Course"));
            menulist.Items.Add(new ListItem("Add Student Records"));
        }
        menulist.Click += (object o, BulletedListEventArgs args) =>
        {
            switch (args.Index)
            {
                case 0:
                    Response.Redirect("AddCourse.aspx");
                    break;
                case 1:
                    Response.Redirect("AddStudent.aspx");
                    break;
            }
        };
    }
}