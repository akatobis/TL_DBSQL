using System.Data.SqlClient;
using HWDataBased.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using HWDataBased.Repositories;

namespace HWDataBased
{
    class Program
    {
        private static string _connectionString = @"Data Source=DESKTOP-65DQT4Q\SQLEXPRESS;Initial Catalog=university;Pooling=true;Integrated Security=SSPI";

        static void Main(string[] args)
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository(_connectionString);
            IGroupRepository groupRepository = new GroupRawSqlRepository(_connectionString);
            IGroupsOfStudents groupsOfStudentRepository = new GroupsOfStudentsRawSqlRepository(_connectionString);

            Console.WriteLine("Доступные команды:");
            Console.WriteLine("add-student - добавить студента");
            Console.WriteLine("add-group - добавить группу");
            Console.WriteLine("add-student-in-group - добавить студента в группу");
            Console.WriteLine("print-students - вывести список студентов");
            Console.WriteLine("print-groups - вывести список групп");
            Console.WriteLine("print-students-by-group-id - вывести студентов по id группы");
            Console.WriteLine("exit - выйти из приложения");
            while( true )
            {
                string command = Console.ReadLine();
                if ( command == "exit" )
                {
                    return;
                }
                else if ( command == "add-student" )
                {
                    Console.WriteLine("Введите имя студента");
                    string name = Console.ReadLine();
                    while (name == "")
                    {
                        Console.WriteLine("Введите имя студента");
                        name = Console.ReadLine();
                    }
                    Console.WriteLine("Введите возраст студента");
                    int age = int.Parse(Console.ReadLine());
                    while ((age < 3) || (age == null))
                    {
                        Console.WriteLine("Убедитесь, что возвраст введён правильно");
                        age = Convert.ToInt32(Console.ReadLine());
                    }

                    studentRepository.AddStudent(new Student
                    {
                        Name = name,
                        Age = age
                    });

                    Console.WriteLine("Студент успешно добавлен");
                }
                else if (command == "add-group")
                {
                    Console.WriteLine("Введите название группы");
                    string name = Console.ReadLine();
                    while (name == "")
                    {
                        Console.WriteLine("Введите название группы");
                        name = Console.ReadLine();
                    }

                    groupRepository.AddGroup(new Group
                    {
                        Name = name,
                    });

                    Console.WriteLine("Группа успешно добавлена");
                }
                else if (command == "add-student-in-group")
                {
                    Console.WriteLine("Введие id студента");
                    int studentId = Convert.ToInt32(Console.ReadLine());
                    Student gotStudentId = studentRepository.GetStudentById(studentId);
                    while (gotStudentId == null)
                    {
                        studentId = Convert.ToInt32(Console.ReadLine());
                        gotStudentId = studentRepository.GetStudentById(studentId);
                    }

                    Console.WriteLine("Введите id группы");
                    int groupId = Convert.ToInt32(Console.ReadLine());
                    Group gotGroupId = groupRepository.GetGroupById(groupId);
                    while (gotGroupId == null)
                    {
                        groupId = Convert.ToInt32(Console.ReadLine());
                        gotGroupId = groupRepository.GetGroupById(groupId);
                    }

                    List<GroupsOfStudents> studentInGroups = groupsOfStudentRepository.GetStudentAndGroupsById();

                    groupsOfStudentRepository.AddStudentInGroup(new GroupsOfStudents
                    {
                        StudentId = studentId,
                        GroupId = groupId
                    });

                    Console.WriteLine("Студент успешно добавлен в группу");
                }
                else if (command == "print-groups")
                {
                    List<Group> groups = groupRepository.GetAllGroups();
                    foreach (Group group in groups)
                    {
                        Console.WriteLine($"Id: {group.Id}, Name: {group.Name}");
                    }
                }
                else if (command == "print-students")
                {
                    List<Student> students = studentRepository.GetAllStudents();
                    foreach (Student student in students)
                    {
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                    }
                } 
                else if (command == "print-students-by-group-id")
                {
                    Console.WriteLine("Введите id группы");
                    int groupsId = Convert.ToInt32(Console.ReadLine());
                    List<GroupsOfStudents> groupsOfStudents = groupsOfStudentRepository.GetAllStudentByGroupId(groupsId);
                    foreach (GroupsOfStudents groupsOfStudent in groupsOfStudents)
                    {
                        //Console.WriteLine($"Id: {groupsOfStudent.StudentId}");
                        Student student = studentRepository.GetStudentById(groupsOfStudent.StudentId);
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                    }
                }
            }
        }
    }
}