using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode()*17 +
                   this.LastName.GetHashCode()*17 +
                   this.FirstMidName.GetHashCode()*17 +
                   this.EnrollmentDate.GetHashCode()*17 +
                   this.Enrollments.GetHashCode()*17;
        }

        public override bool Equals(object? obj)
        {
            return this == (Student)obj;
        }

        public static Boolean operator ==(Student a, Student b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            else
            {
                return a.Id == b.Id &&
                       a.LastName == b.LastName &&
                       a.FirstMidName == b.FirstMidName &&
                       a.EnrollmentDate == b.EnrollmentDate &&
                       a.Enrollments == b.Enrollments;
            }
        }
        
        public static Boolean operator !=(Student a, Student b)
        {
            return !(a == b);
        }
    }
}