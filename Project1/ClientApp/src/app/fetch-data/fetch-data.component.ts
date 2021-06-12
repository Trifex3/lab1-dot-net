import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {//baseUrl face un get prin http get baseurl+weatherforecast
                                                                        // e un client care vine din angular pur si simplu asta e sintaxa
    //baseurl un parametru injectata in constructorul compunenetei
    http.get<WeatherForecast[]>(apiUrl + 'weatherforecast').subscribe(result => {//hpptp get face un request de tip get
      this.forecasts = result;//intre<> tipul pe care ma stept sa il returneze, un array de wrather forecast; parametrul e adrsa catre care se face request ul
                                //get-ul returneaza un observable=ceva observabil care poate fi observat de observable; notifica observatorii atunci cand se intmpla ceva cand arre date; un observable trebuie sa se inscrie la observabali
                                //prin subscribe facem acea subscriibtie; vrem sa fim notificati cand vine raspunsul de la get(nu vrem sa blocam imaginea pana se face acest lucru)
                                //in subscribe se da ca parametru o functie landa aici, cares e indetifica cand exista noutati
                                //in cazul acesta functia ia ca parametru result adica ce returneaza get-ul adica un array de weatherforecast si tot ce face este sa seteze thi.forecast ca fiind rezultatul
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
