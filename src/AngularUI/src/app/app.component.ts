import { Component, Inject } from '@angular/core';
import { DataService } from '../services/data.service';
import { DataModel } from './models/DataModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  public title = 'YARP Application';

  public data: DataModel = { source: '', data: '' };

  public endpoints: string[] = ['legacyWebapp', 'webapp'];

  public selectedEndpoint: string = "";

  public showModal: boolean = false;

  public fullPath: string = "";

  private baseUrl: string;



  constructor(private dataService: DataService, @Inject('baseUrl') baseUrl?: string) {
    this.baseUrl = baseUrl ? baseUrl : "";
  }

  getData(endpoint: string) {
    this.dataService.getData(endpoint).subscribe(
      {
        next: (result: DataModel)=>{
          this.data = result;
          this.selectedEndpoint = endpoint;
          this.fullPath = this.getFullPath(endpoint);
          this.showModal = true;
        },
        error: (error)=>{
          console.error('Error fetching data:', error);
        }
      });
  }

  getFullPath(endpoint: string){
    return `${this.baseUrl}/${endpoint}`;
  }

  closeModal(){
    this.showModal = false;
  }

}


