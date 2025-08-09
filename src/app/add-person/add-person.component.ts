import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PersonService, Person } from '../services/person.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-person',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add-person.component.html',
  styleUrls: ['./add-person.component.css']
})
export class AddPersonComponent {
  personForm: FormGroup;

    emailSuggestions: string[] = [];
  knownDomains = ["gmail.com", "walla.co.il", "hotmail.com", "outlook.com", "yahoo.com"];

  constructor(
    private fb: FormBuilder,
    private personService: PersonService,
    private router: Router
  ) {
    this.personForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      identityNumber: ['', [Validators.required, Validators.pattern(/^\d{9}$/)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]]
    });
     this.personForm.get('email')?.valueChanges.subscribe(value => {
      this.updateEmailSuggestions(value);
    });
  }
    updateEmailSuggestions(email: string) {
    if (!email || !email.includes('@')) {
      this.emailSuggestions = [];
      return;
    }
    const parts = email.split('@');
    if (parts.length !== 2) {
      this.emailSuggestions = [];
      return;
    }
    const prefix = parts[0];
    const typedDomain = parts[1].toLowerCase();

    this.emailSuggestions = this.knownDomains
      .filter(d => d.startsWith(typedDomain))
      .map(d => `${prefix}@${d}`);
  }

  applySuggestion(suggestion: string) {
    this.personForm.patchValue({ email: suggestion });
    this.emailSuggestions = [];
  }

  onSubmit(): void {
    if (this.personForm.valid) {
      const person: Person = this.personForm.value;
      this.personService.addPerson(person).subscribe({
        next: () => {
          alert('האדם נוסף בהצלחה');
          this.router.navigate(['/']);
        },
        error: (err) => console.error('שגיאה בהוספת האדם', err)
      });
    }
  }
  onEmailInput(event: Event): void {
  const input = event.target as HTMLInputElement;
  input.value = input.value.replace(/[\u0590-\u05FF]/g, '');
  this.personForm.get('email')?.setValue(input.value, { emitEvent: false });
}

}