import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {PersonService} from '../person.service';
import {Person} from '../person';

@Component({
  selector: 'app-person-create',
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent {
  person: Person = new Person();

  constructor(private personService: PersonService, private router: Router) {
  }

  createPerson(): void {
    this.personService.createPerson(this.person).subscribe(() => {
      this.router.navigate(['/people']);
    });
  }
}
