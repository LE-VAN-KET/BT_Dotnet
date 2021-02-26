using System;

namespace BT_NET {
    class QLSV {
        public SV[] studentList { get; set; }
        public int size { get; set; }

        public QLSV() {
            size = 0;
            studentList = null;
        }

        // Add students into last list
        public void Add(SV student) {
            if (size == 0) {
                studentList = new SV[] { student };
                size++;
            } else {
                SV[] studentListTemp = studentList;
                studentList = new SV[size + 1];
                for (int i = 0; i < size; ++i) {
                    studentList[i] = studentListTemp[i];
                }
                studentList[size] = student;
                size++;
            }
        }

        // Insert students into positive index list
        public void Insert(SV student, int index) {
            SV[] studentListTemp = studentList;
            studentList = new SV[size + 1];
            // for (int i = 0; i < index; ++i) {
            //     studentList[i] = studentListTemp[i];
            // }
            copyStudentListFrom_OneToTwo(studentListTemp, studentList, index);
            studentList[index] = student;
            for (int i = index + 1; i < size + 1; ++i) {
                studentList[i] = studentListTemp[i - 1];
            }
            size++;
        }

        // Search student if Equals return position index else return -1
        public int IndexOf(SV student) {
            int index = -1;
            for(int i = 0; i < size; ++i) {
                if (studentList[i].Equals(student) == true) return i;
            }
            return index;
        }

        // coppy from list students One to list students Two with size options
        public void copyStudentListFrom_OneToTwo(SV[] studentOne, SV[] studentTwo, int sizeOfStudents) {
            for (int i = 0; i < sizeOfStudents; ++i) {
                studentTwo[i] = studentOne[i];
            }
        }

        // Search students if equals then remove from list
        public void Remove(SV student) {
            for (int i = 0; i < size; ++i) {
                if (studentList[i].Equals(student)) {
                    RemoveAt(i);
                }
            }
        }

        //  Remove student at position index inside list
        public void RemoveAt(int index) {
            SV[] listStudentTemp = new SV[size];
            copyStudentListFrom_OneToTwo(studentList, listStudentTemp, size);
            studentList = new SV[size - 1];
            copyStudentListFrom_OneToTwo(listStudentTemp, studentList, index);
            for (int i = index; i < size - 1; ++i) {
                studentList[i] = listStudentTemp[i + 1];
            }
            size = size - 1;
        }

        // Search student By MSSV if found return index
        public int SearchByMSSV(int MSSV) {
            int index = -1;
            for (int i = 0; i < size; ++i) {
                if (studentList[i].MSSV == MSSV) {
                    return i;
                }
            }
            return index;
        }

        // Modify student infomation By MSSV
        public void ModifyByMSSV(int MSSV) {
            int index = SearchByMSSV(MSSV);
            if (index != -1) {
                Console.WriteLine("------Modify Student Infomation.------");
                Console.WriteLine("    1. Modify Name Student");
                Console.WriteLine("    2. Modify DTB Student");
                int chooseIndex;
                Console.Write("Chon: ");
                int.TryParse(Console.ReadLine(), out chooseIndex);
                switch (chooseIndex) {
                    case 1:
                        String nameStudent;
                        Console.Write("Name Student New: ");
                        nameStudent = Convert.ToString(Console.ReadLine());
                        studentList[index].NameSV = nameStudent;
                        break;
                    case 2:
                        double _DTB;
                        Console.Write("DTB New: ");
                        double.TryParse(Console.ReadLine(), out _DTB);
                        studentList[index].DTB = _DTB;
                        break;
                    default:
                        Console.WriteLine("Error: solution not  invalid !");
                        break;
                }
            } else throw new Exception("MSSV not found");
        }

        public override string ToString() {
            string results = "";
            foreach (SV student in studentList) {
                results += student.ToString() + "\n";
            }
            return results;
        }

        public void swap<T> (ref T item_1, ref T item_2) {
            T temp = item_1;
            item_1 = item_2;
            item_2 = temp;
        }

        public String splitNameSV(String NameSV) {
            String[] _NameSV = NameSV.Split(' ');
            return _NameSV[_NameSV.Length - 1];
        }

        public bool compare(String NameSV_1, String NameSV_2) {
            String sv1 = splitNameSV(NameSV_1);
            String sv2 = splitNameSV(NameSV_2);
            return (sv1.CompareTo(sv2) > 0) ? true : false;
        }

        public bool down(String NameSV_1, String NameSV_2) {
            return (compare(NameSV_1, NameSV_2) == false) ? true : false;
        }

        public bool up(String NameSV_1, String NameSV_2) {
            return (compare(NameSV_1, NameSV_2) == true) ? true : false;
        }

        public delegate bool quickSort(String NameSV_1, String NameSV_2);

        // Sort list students by Name students
        public void sortByNameSV(quickSort callback, int start, int end) {
            int left = start, right = end - 1;
            if (left <= right) {
                String pivot = studentList[(start + end) / 2].NameSV;
                while (left <= right) {
                    while (callback(studentList[right].NameSV, pivot)) ++left;
                    while (callback(studentList[right].NameSV, pivot)) --right;
                    if (left < right) {
                        swap<SV>(ref studentList[left], ref studentList[right]);
                    }
                    left++; right--;
                }
                if (start < right) sortByNameSV(callback, start, right);
                if (left < end) sortByNameSV(callback, left, end);
            }
        }

        public void sortStudents() {
            Console.WriteLine("\t\tSorting students...");
            Console.WriteLine("\t\t1. Sort Name Student increase");
            Console.WriteLine("\t\t2. Sort Name Student decrease");
            int choose;
            Console.Write("\t\tChon: ");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose) {
                case 1:
                    sortByNameSV(up, 0, size);
                    Console.WriteLine("\tSort successfully...");
                    break;
                case 2:
                    sortByNameSV(down, 0, size);
                    Console.WriteLine("\tSort successfully...");
                    break;
                default:
                    Console.WriteLine("\tError: solution not invalid");
                    break;
            }
        }

        public void AddStudents() {
            Console.WriteLine("\t\tAdd students...");
            Console.WriteLine("\t\t1. Add student into last list");
            Console.WriteLine("\t\t2. Add student into position index");
            int choose;
            Console.Write("\t\tChon: ");
            int.TryParse(Console.ReadLine(), out choose);
            int _MSSV;
            String _NameSV;
            double _DTB;
            switch (choose) {
                case 1:
                    try {
                        Console.Write("\t\tMSSV: ");
                        _MSSV = Int32.Parse(Console.ReadLine());
                        Console.Write("\t\tName Student: ");
                        _NameSV = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tDTB: ");
                        _DTB = Convert.ToDouble(Console.ReadLine());
                        SV student = new SV();
                        student.MSSV = _MSSV;
                        student.NameSV = _NameSV;
                        student.DTB = _DTB;
                        Add(student);
                    } catch(FormatException f) {
                        Console.WriteLine("\t\tERROR: MSSV must is number and DTB must is decimal");
                    }
                    break;
                case 2:
                    int index;
                    try {
                        Console.Write("\t\tMSSV: ");
                        _MSSV = Int32.Parse(Console.ReadLine());
                        Console.Write("\t\tName Student: ");
                        _NameSV = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tDTB: ");
                        _DTB = Double.Parse(Console.ReadLine());
                        Console.Write("\t\tposition index insert: ");
                        index = Int32.Parse(Console.ReadLine());
                        if (index < 0 || index >= size) {
                            throw new Exception("position index out of array");
                        }
                        SV student = new SV();
                        student.MSSV = _MSSV;
                        student.NameSV = _NameSV;
                        student.DTB = _DTB;
                        Insert(student, index);

                    } catch(FormatException) {
                        Console.WriteLine("\t\tERROR: MSSV and DTB and index must is number");
                    } catch(Exception e) {
                        Console.WriteLine("\t\tERROR: {0}", e.Message);
                    }
                    break;
                default:
                    Console.WriteLine("\tError: solution not invalid");
                    break;
            }
        }

        public void ShowStudents() {
            if (size == 0) {
                Console.WriteLine("Not have students");
                return;
            }
            Console.WriteLine(ToString());
        }

        public void ModifyStudents() {
            Console.WriteLine("\t\tModify students...");
            try {
                Console.Write("Search MSSV: ");
                int _MSSV;
                _MSSV = Int32.Parse(Console.ReadLine());
                ModifyByMSSV(_MSSV);
            } catch (FormatException) {
                Console.WriteLine("\t\tERROR: MSSV not invalid must is number");
            } catch (Exception e) {
                Console.WriteLine("\t\tERROR: {0}", e.Message);
            }
        }

        public SV SearchStudents() {
            Console.WriteLine("\t\tSearch students...");
            Console.WriteLine("\t\t1. Search by MSSV");
            Console.WriteLine("\t\t2. Search by all students infomation");
            int choose;
            Console.Write("\t\tChon: ");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose) {
                case 1:
                    try {
                        Console.Write("Search MSSV: ");
                        int _MSSV;
                        _MSSV = Int32.Parse(Console.ReadLine());
                        int index = SearchByMSSV(_MSSV);
                        if (index < 0 || index > size) {
                            throw new Exception("MSSV not found");
                        }
                        return studentList[index];
                    } catch (FormatException) {
                        Console.WriteLine("ERROR: MSSV must is number");
                    }
                    catch (Exception e) {
                        Console.WriteLine("\t\tERROR: {0}", e.Message);
                    }
                    break;
                case 2:
                    try {
                        int _MSSV;
                        String _NameSV;
                        double _DTB;
                        Console.Write("\t\tMSSV: ");
                        _MSSV = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\t\tName Student: ");
                        _NameSV = Convert.ToString(Console.ReadLine());
                        Console.Write("\t\tDTB: ");
                        _DTB = Double.Parse(Console.ReadLine());
                        SV student = new SV();
                        student.MSSV = _MSSV;
                        student.NameSV = _NameSV;
                        student.DTB = _DTB;
                        int index = IndexOf(student);
                        if (index == -1) throw new Exception("Student not found");
                        return studentList[index];
                    } catch (FormatException) {
                        Console.WriteLine("\t\tERROR: MSSV and DTB must is number");
                    } 
                    catch (Exception e) {
                        Console.WriteLine("\t\tERROR: {0}", e.Message);
                    }
                    break;
                default:
                    Console.WriteLine("\t\tsolution not invalid");
                    break;
            }
            return null;
        }

        public void DeleteStudents() {
            Console.WriteLine("\t\tRemove studenets...");
            Console.WriteLine("\t\t1.Remove student At index");
            Console.WriteLine("\t\t2.Remove student By students infomation");
            int choose;
            Console.Write("\t\tChon: ");
            int.TryParse(Console.ReadLine(), out choose);
            switch (choose) {
                case 1:
                    int index;
                    try {
                        Console.Write("\t\tPosition index: ");
                        index = Int32.Parse(Console.ReadLine());
                        if (index < 0 || index >= size) {
                            throw new Exception("position index out of array");
                        }
                        RemoveAt(index);
                    } catch(FormatException) {
                        Console.WriteLine("\t\tERROR: Index must is number");
                    } catch(Exception e) {
                        Console.WriteLine("\t\tERROR: {0}", e.Message);
                    }
                    break;
                case 2:
                    SV student = SearchStudents();
                    if (student != null) {
                        Remove(student);
                    }
                    break;
                default:
                    Console.Write("ERROR: solution of you not invalid");
                    break;
            }
        }
    }
}