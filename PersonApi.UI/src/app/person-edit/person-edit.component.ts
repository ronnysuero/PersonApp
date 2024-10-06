import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PersonService} from '../person.service';
import {Person} from '../person';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {
  person: Person = new Person();

  constructor(
    private route: ActivatedRoute,
    private personService: PersonService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.personService.getPerson(id).subscribe(data => {
      this.person = data;
    });
  }

  updatePerson(): void {
    this.personService.updatePerson(this.person).subscribe(() => {
      this.router.navigate(['/people']);
    });
  }
}
