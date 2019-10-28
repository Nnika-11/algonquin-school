






    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Default2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        lblErrNum.Text = "";
        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
            btnHome.Click += (o, a) => Response.Redirect("Default.aspx");

            BulletedList menulist = (BulletedList)Master.FindControl("topMenu");
            if (!IsPostBack)
            {
                menulist.Items.Add(new ListItem("Add Course"));
                menulist.Items.Add(new ListItem("Add Student Records"));
            }
            menulist.Click += (o, a) => { if (a.Index == 1) Response.Redirect("AddStudent.aspx"); };

            using(var entityContext = new StudentRecordEntities()) 
            {
               
                var courses = (from course in entityContext.Courses
                               orderby course.Code
                               select course).ToList();
            }

            DisplayCourses();
        }
    protected void ExistanceValidator(object sender, ServerValidateEventArgs args)
    {
        using (var entityContext = new StudentRecordEntities())
        {
            List<Course> courses = entityContext.Courses.ToList<Course>();
            foreach (Course cr in courses)
            {

                if (cr.Code == courseNum.Text)
                {
                    
                    args.IsValid = false;
                    break;
                }
            }
        }
    }

    protected void btnAddCrsInfo(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (var entityContext = new StudentRecordEntities())
            {
                List<Course> courses = entityContext.Courses.ToList<Course>();
                /*      bool valid = true;
                     foreach(Course cr in courses)
                 {

                     if(cr.Code == courseNum.Text)
                     {
                         lblErrNum.Text = "Course with this code already exist";
                         valid = false;
                         break;
                     }
                 }
                 if (valid)
                 {     */
                Course course = new Course();
                course.Code = courseNum.Text;
                course.Title = courseName.Text;
                entityContext.Courses.Add(course);
                entityContext.SaveChanges();
                DisplayCourses();
            }
        }
    }
           
        
        private void DisplayCourses()
        {
        while (tblCourseInfo.Rows.Count > 1) tblCourseInfo.Rows.RemoveAt(1);
        using (var entityContext = new StudentRecordEntities())
        {
            List<Course> courses = entityContext.Courses.ToList<Course>();

            string sort = Request.Params["sort"];
            if (sort == "code")
            {
                if (Session["order"] == null || Session["order"].ToString() == "asc")
                {
                    courses.Sort((x, y) => string.Compare(x.Code, y.Code));
                    Session["order"] = "desc";
                }
                else
                {
                    courses.Sort((x, y) => string.Compare(y.Code, x.Code));
                    Session["order"] = "asc";
                }
            }
            if (sort == "title")
            {
                if (Session["order"] == null || Session["order"].ToString() == "asc")
                {
                    courses.Sort((x, y) => string.Compare(x.Title, y.Title));
                    Session["order"] = "desc";
                }
                else
                {
                    courses.Sort((x, y) => string.Compare(y.Title, x.Title));
                    Session["order"] = "asc";
                }
            }

            foreach (Course course in courses){
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = course.Code;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = course.Title;
                row.Cells.Add(cell);
                tblCourseInfo.Rows.Add(row);
            }
        }
        }
    }
