using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Click += (o, a) => Response.Redirect("Default.aspx");

        BulletedList menulist = (BulletedList)Master.FindControl("topMenu");
        if (!IsPostBack)
        {
            menulist.Items.Add(new ListItem("Add Course"));
            menulist.Items.Add(new ListItem("Add Student Records"));

        }
        menulist.Click += (o, a) => { if (a.Index == 0) Response.Redirect("AddCourse.aspx"); };


        using (var entityContext = new StudentRecordEntities())
        {

            var courses = (from course in entityContext.Courses
                           orderby course.Code
                           select course).ToList();

            if (!IsPostBack)
            {
                foreach (Course course in courses)
                {
                    ListItem item = new ListItem(course.Code + " - " + course.Title);
                    drpCourseSelection.Items.Add(item);
                    Session["selectedcourseName"] = "";
                    Session["selectedcourseCode"] = "";
                }
                DisplayStdn();
            }
        }
    }


    protected void ExistanceValidator(object sender, ServerValidateEventArgs args)
    {
        using (var entityContext = new StudentRecordEntities())
        {
            string courseID = Session["selectedcourseCode"].ToString();
            List<AcademicRecord> StidCheck = (from academRecord in entityContext.AcademicRecords
                                              where academRecord.StudentId == stdId.Text &&
                                              academRecord.CourseCode == courseID
                                              select academRecord
                         ).ToList();
            

             if (StidCheck.Count != 0)
                    args.IsValid = false;
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (var entityContext = new StudentRecordEntities())
        {
            if (Page.IsValid)
            {
                
                List<Student> students = entityContext.Students.ToList<Student>();
                List<AcademicRecord> academRecords = entityContext.AcademicRecords.ToList<AcademicRecord>();
                bool stExist=false;
                foreach (Student student in students)
                {
                    if (student.Id == stdId.Text)
                    {
                        AcademicRecord academRecord = new AcademicRecord();
                        academRecord.CourseCode = Session["selectedcourseCode"].ToString();
                        academRecord.Student = student;
                        academRecord.Grade = Int32.Parse(stdGrade.Text);
                        entityContext.AcademicRecords.Add(academRecord);
                        entityContext.SaveChanges();
                        stExist = true;
                        break;

                    }
                }
                    if(!stExist)
                    {
                        Student std = new Student();
                        std.Id = stdId.Text;
                        std.Name = stdName.Text;
                        entityContext.Students.Add(std);
                        AcademicRecord academRecord = new AcademicRecord();
                        academRecord.CourseCode = Session["selectedcourseCode"].ToString();
                        academRecord.Student = std;
                        academRecord.Grade = Int32.Parse(stdGrade.Text);
                        entityContext.AcademicRecords.Add(academRecord);
                        entityContext.SaveChanges();
                    }
                

                
            }
        }
        DisplayStdn();
    }


    protected void drpCourseSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["selectedcourseName"] = drpCourseSelection.SelectedValue.Split('-')[1].Trim();
        Session["selectedcourseCode"] = drpCourseSelection.SelectedValue.Split('-')[0].Trim();
        DisplayStdn();
    }
    private void DisplayStdn()
    {
        while (tblStudentInfo.Rows.Count > 1) tblStudentInfo.Rows.RemoveAt(1);
        using (var entityContext = new StudentRecordEntities())
        {

            var academRecords = (from academRecord in entityContext.AcademicRecords
                                 orderby academRecord.Student.Id
                                 select academRecord).ToList();
            bool isExist = false;


            if (drpCourseSelection.SelectedIndex != 0)
            {
                foreach (AcademicRecord academRecord in academRecords)
                {
                    var a = academRecord.Course.Title;
                    var b = Session["selectedcourseName"].ToString();
                    if (academRecord.Course.Title.Trim() != Session["selectedcourseName"].ToString())
                    {
                        continue;
                    }
                    else
                    {
                        isExist = true;
                        Student student = academRecord.Student;
                        TableRow row = new TableRow();

                        TableCell cell = new TableCell();
                        cell.Text = student.Id;
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = academRecord.Student.Name;
                        row.Cells.Add(cell);

                        cell = new TableCell();
                        cell.Text = academRecord.Grade + "";
                        row.Cells.Add(cell);

                        tblStudentInfo.Rows.Add(row);
                    }
                }
            }
            if (!isExist)
            {
                TableRow lastRow = new TableRow();
                TableCell lastRowCell = new TableCell();
                lastRowCell.Text = "No Courses record exist";
                lastRowCell.ForeColor = System.Drawing.Color.Red;
                lastRowCell.ColumnSpan = 3;
                lastRowCell.HorizontalAlign = HorizontalAlign.Center;
                lastRow.Cells.Add(lastRowCell);
                tblStudentInfo.Rows.Add(lastRow);
            }
        }
    }

   
}