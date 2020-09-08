using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public class GradeSchool
{
    public int Grades { get; set; }
    public string Student { get; set; }

    public List<GradeSchool> GradesStudents = new List<GradeSchool>();

    public void Add(string student, int grade) =>
        GradesStudents.Add(new GradeSchool
        {
            Grades= grade,
            Student = student
        });

    public IEnumerable<string> Roster() => GradesStudents.OrderBy(user => user.Grades).ThenBy(q => q.Student).Select(q => q.Student);

    public IEnumerable<string> Grade(int grade) =>
        GradesStudents.Any(q => q.Grades == grade) 
            ? GradesStudents.Where(q => q.Grades == grade).OrderBy(user => user.Grades).ThenBy(q => q.Student).Select(q => q.Student)
            : new List<string>();
}