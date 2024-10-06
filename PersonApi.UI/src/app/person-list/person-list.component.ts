import {Component, OnInit} from '@angular/core';
import {PersonService} from '../person.service';
import {Person} from '../person';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  people: Person[] = [];

  constructor(private personService: PersonService) {
  }

  ngOnInit(): void {
    this.personService.getPeople().subscribe(data => {
      this.people = data;
    });
  }

  deletePerson(id?: number): void {
    this.personService.deletePerson(id).subscribe(() => {
      this.people = this.people.filter(p => p.id !== id);
    });
  }
}
