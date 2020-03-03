# End of Day 1 Project

## Project

We're going to make a simple HR database. It will

1. Hold information about employees
2. Allow simple report generation
3. Allow the user to dump the database to a file

### The File

The file will be in a specific format

- The first line will contain the number of employees as a single int
- The next lines will contain information about the employees, space separated. It will be of the format
  1. Department (Guaranteed to be one of `Engineering`, `Product`, or `Adminstration`)
  2. First name
  3. Last name
  4. Year of hire
  5. An int representing management status
    - 0 - Not a manager
    - 1 - Team Lead
    - 2 - Director
    - 3 - Executive

#### Example file

```
3
Engineering Employee One 2009 1
Product Monty Productmanager 2010 0 
Administration Tom CEO 2008 3
```

## What to implement

### Domain models

- Create an `abstract class` called `Employee` along with three derived classes, `Admin`, `Engineer`, and `Product`.
- Create a `ManagementStatus` `enum`
- Give `Employee` properties for
  1. First name
  2. Last name
  3. Year of hire
  4. Management status
- Add an abstract method called `SayWhatIDo()`
  - `SayWhatIDo()` should print a sentence to the console of the form
  - "Hello my name is {First name} {Last name}. I'm in the {department} department. I {am/am not} a manager. My duties include {what they do}."
- Add a constructor on `Employee` that takes in the four properties
- Implement `Admin`, `Engineer`, and `Product`
  - You may want to implement `ToString()`

### Use of the app

- Ask the user for a file name. Assume the file exists at that location.
- Read the file
  - Create an array of `Employee` that will store the employees
  - Add each employee to the array using their corresponding class
- Display a menu with these options
  - 1 Show all managers
  - 2 Show all non managers
  - 3 Show all Engineers
  - 4 Show all members of the Product Team
  - 5 Show all Admins
  - 6 Have employees explain their job
  - 7 Dump database to file
  - Q Quit
- Allow the user to select an option
  - If their input is invalid, say so and then display the menu again

Implement each of the functions of the menu. 

Options 1-5 should read through the array of employees, find the correct employees, and display them to the console.

Option 6 should loop through the array of employees and call `SayWhatIDo()` on each.

Option 7 should ask the user for a file name, then print the contents of the array to that file **in a machine readable format**.

The quit option should accept Q, q, quit, or Quit

## Some helpful resources

This project is supposed to be a little bit more complicated, so please feel free to ask for help.

[Reading a file](https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=netframework-4.8)
[Writing a file](https://docs.microsoft.com/en-us/dotnet/api/system.io.file.writealllines?view=netframework-4.8)

## Extra credit

1. Add options to sort the array (by last name, by department, etc)
2. Properly handle `FileNotFound` exceptions