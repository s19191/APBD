using LinqCwiczenia.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LinqCwiczenia
{
    public partial class Form1 : Form
    {
        public IEnumerable<Emp> Emps { get; set; }
        public IEnumerable<Dept> Depts { get; set; }

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;
            ResultsDataGridView.DataSource = Emps;

            #endregion

        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        private void Przyklad1Button_Click(object sender, EventArgs e)
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select emp;


            //2. Lambda and Extension methods
            var res2 = Emps
                .Where((emp, indx) => emp.Job == "Backend programmer");

            ResultsDataGridView.DataSource = res.ToList();
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        private void Przyklad2Button_Click(object sender, EventArgs e)
        {
            /*var res = (from emp in Emps
                       join dept in Depts on emp.Deptno equals dept.Deptno
                       where emp.Job == "Frontend programmer" && emp.Salary > 1000
                       orderby emp.Ename descending
                       select emp).ToList(); */

            var res2 = Emps
                .Where((emp, indx) => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                .OrderByDescending(emp => emp.Ename);


            ResultsDataGridView.DataSource = res2.ToList();
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        private void Przyklad3Button_Click(object sender, EventArgs e)
        {
            var max = Emps
                .Max(emp => emp.Salary);

            WynikTextBox.Text = max + "";
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        private void Przyklad4Button_Click(object sender, EventArgs e)
        {
            var res = Emps
                .Where(emp => emp.Salary == Emps.Max(emp1 => emp1.Salary))
                .Select(emp => emp);

            ResultsDataGridView.DataSource = res.ToList();
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        private void Przyklad5Button_Click(object sender, EventArgs e)
        {
            var res = Emps.
                Select(emp => new 
                { 
                    Nazwisko = emp.Ename, 
                    Praca = emp.Job 
                });

            ResultsDataGridView.DataSource = res.ToList();
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        private void Przyklad6Button_Click(object sender, EventArgs e)
        {
            var res = Emps.
                Join(Depts,
                emp => emp.Deptno,
                dept => dept.Deptno,
                (emp, dept) => new 
                { 
                    emp.Ename, 
                    emp.Job, 
                    dept.Dname 
                });

            ResultsDataGridView.DataSource = res.ToList();
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        private void Przyklad7Button_Click(object sender, EventArgs e)
        {
            var res = Emps.
                GroupBy(emp => emp.Job).
                Select(emp => new
                {
                    Praca = emp.Key,
                    LiczbaPracownikow = emp.ToList().Count()
                });

            ResultsDataGridView.DataSource = res.ToList();
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        private void Przyklad8Button_Click(object sender, EventArgs e)
        {
            if (Emps.Any(emp => emp.Job == "Backend programmer"))
            {
                WynikTextBox.Text = "Backend programmer istnieje w kolekcji";
            } else
            {
                WynikTextBox.Text = "Backend programmer nie istnieje w kolekcji";
            }
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        private void Przyklad9Button_Click(object sender, EventArgs e)
        {
            var res = Emps
                .Where((emp) => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .Take(1);

            ResultsDataGridView.DataSource = res.ToList();
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        private void Przyklad10Button_Click(object sender, EventArgs e)
        {
            
             var res = Emps
                 .Select(emp => new
                 {
                     emp.Ename,
                     emp.Job,
                     emp.HireDate
                 }).Union(Emps
                     .Select(emp => new
                     {
                         Ename = "Brak wartości",
                         Job = (string?) null,
                         HireDate = (DateTime?) null
                     }));
             
            // to nie działa, ale nie wiem dlaczego w drugim projekcie (LinqConsoleApp) działa

            /*
            var res = (from emp in Emps select emp)
                .Union(from emp in Emps select emp.Ename == "Brak wartości" && emp.Job == (string)null && emp.HireDate == (DateTime?) null);
            */
           // to również nie działa

           //ResultsDataGridView.DataSource = res.ToList();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        private void Przyklad11Button_Click(object sender, EventArgs e)
        {
            var res = Emps
                .Aggregate((s1, s2) =>
                {
                    if (s1.Salary > s2.Salary)
                    {
                        return s1;
                    }
                    else
                    {
                        return s2;
                    }
                });

            WynikTextBox.Text = res + " ";
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        private void Przyklad12Button_Click(object sender, EventArgs e)
        {
            var res = Emps.CrossJoin(Depts);
            // tu coś dziwnie to wygląda w deps, ale nie wiem jak to naprawić

            ResultsDataGridView.DataSource = res.ToList();
        }
    }
}
