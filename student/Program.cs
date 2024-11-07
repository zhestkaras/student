using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
public class Student
{
    public string Fullname { get; set; }
    public int Birthyear { get; set; }
    public Group Group { get; set; }
      public Student(string fullname , int birthyear, Group group)
      {
    Fullname = fullname;
    Birthyear = birthyear;
    Group = group;
      }
}
public class Group
{
    public string Namber { get; set; }
    public Special Speciality { get; set; }
    public Group(string namber, Special speciality)
    {
        Namber = namber;
        Speciality = speciality;
    }
}
public class Special
{
    public string Name { get; set; }
    public Special(string name)
    {
      Name = name;
    }
}
public static class Model
{
    public static Special CreateSpecial( string name)
    {
        return new Special(name);
    }
    public static Group CreateGroup(string namber, Special speciality)
    {
        return new Group(namber, speciality);
    }
    public static Student Createstudent(string fullname, int birthyear, Group group)
    { 
        return new Student(fullname, birthyear, group);
    }
    public static void Display(Student student)
    {
        Console.WriteLine($"Fullname: {student.Fullname}");
        Console.WriteLine($"bithear:{student.Birthyear}");
        Console.WriteLine($"group:{student.Group.Namber}");
        Console.WriteLine($"speality: {student.Group.Speciality}");
        Console.WriteLine();
    }
}

public class DB
{
    public List<Student> students = new List<Student>();
    public void AddStudent(Student student)
    {
        students.Add(student);
    }
    public void SaveFile(string filePath)
    {
        var json = JsonSerializer.Serialize(students);
        File.WriteAllText(filePath, json);
    }
    public DB(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            students = JsonSerializer.Deserialize<List<Student>>(json);
        }
    }
    public Student this[int index]
    {
        get => students[index];
        set => students[index] = value;
    }
}
    class Program
    {
        static void Main()
        {
            var speciality = Model.CreateSpecial("infor");
            var group = Model.CreateGroup("101",  speciality);
            var student1 = Model.Createstudent("ivan", 2000, group);
            var student2 = Model.Createstudent("petr", 2001, group);
            var db = new DB("students.json");
            db.AddStudent(student1);
            db.AddStudent(student2);
            db.SaveFile("students.json");
            for (int i = 0; i < 2; i++)
            {
                Model.Display(db[i]);
            }
        }
    }

