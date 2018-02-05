using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

    private enum TAG {
        Code, Production, Sound, Art
    }

    private struct Task {
        public TAG tag;
        public string[] tasks;

        public Task(TAG tag, params string[] tasks) {
            this.tag = tag;
            this.tasks = tasks;
        }
    }

    private class Person {

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }
        private string name;

        public List<Task> Tasks
        {
            get { return tasks; }
            private set { tasks = value; }
        }
        private List<Task> tasks;

        public Person(string name, params Task[] tasks) {
            Name = name;
            Tasks = new List<Task>(tasks);
        }
    }

    private List<Person> persons = new List<Person>();

    public Text codeText;
    public Text productionText;
    public Text artText;
    public Text soundText;

    // Use this for initialization
    void Start() {
        persons.Add(new Person("Adelina Lintuluoto", new Task(TAG.Code, "Code")));
        persons.Add(new Person("Christoffer Fridlund", new Task(TAG.Code, "Code")));
        persons.Add(new Person("Jeremias Berg", new Task(TAG.Production, "Production"), new Task(TAG.Code, "Code")));
        persons.Add(new Person("Tom Olander", new Task(TAG.Code, "Code")));
        persons.Add(new Person("Paul Vuorela", new Task(TAG.Code, "Code"), new Task(TAG.Sound, "Music")));
        persons.Add(new Person("Dan Lindholm", new Task(TAG.Sound, "Music", "SoundFX")));
        persons.Add(new Person("Mattias Lassheikki", new Task(TAG.Art, "Modelling", "LVL Design")));
        persons.Add(new Person("Meri Sillanpää", new Task(TAG.Art, "3D Textures")));
        persons.Add(new Person("Christina Lassheikki", new Task(TAG.Art, "2D & UI art")));
        persons.Add(new Person("Göran Maconi", new Task(TAG.Art, "Modelling")));

        // Populate the different text objects with the proper names
        foreach (Person person in persons) {
            foreach (Task task in person.Tasks) {
                string taskInfo = "";
                foreach (string t in task.tasks) {
                    taskInfo += " " + t + ",";
                }
                taskInfo = taskInfo.Trim(' ');
                taskInfo = taskInfo.Trim(',');
                switch (task.tag) {
                    case TAG.Art:
                        artText.text += person.Name + "\n" + taskInfo + "\n";
                        break;
                    case TAG.Code:
                        codeText.text += person.Name + "\n" + taskInfo + "\n";
                        break;
                    case TAG.Production:
                        productionText.text += person.Name + "\n" + taskInfo + "\n";
                        break;
                    case TAG.Sound:
                        soundText.text += person.Name + "\n" + taskInfo + "\n";
                        break;
                }
            }
        }
    }
    
}
