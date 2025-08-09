import { Component, OnInit } from '@angular/core';
import { PersonService, Person } from '../services/person.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-people-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.css']
})
export class PeopleListComponent implements OnInit {

  people: Person[] = [];

  constructor(private personService: PersonService) {}

  ngOnInit(): void {
    this.loadPeople();
  }


  loadPeople(): void {
  this.personService.getPeople().subscribe({
    next: (data) => {
      this.people = data;
      console.log('אנשים נטענו:', this.people);
    },
    error: (err) => console.error('שגיאה בטעינת האנשים', err)
  });
}

  deletePerson(id: number): void {
    if (confirm('האם אתה בטוח שברצונך למחוק אדם זה?')) {
      this.personService.deletePerson(id).subscribe({
        next: () => {
          this.people = this.people.filter(person => person.id !== id);
          alert('האדם נמחק בהצלחה');
        },
        error: (err) => console.error('שגיאה במחיקת האדם', err)
      });
    }
  }
}