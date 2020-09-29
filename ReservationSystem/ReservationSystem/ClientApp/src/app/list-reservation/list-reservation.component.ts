import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Users} from '../../models/User';
import {newArray} from '@angular/compiler/src/util';
import {HeaderInfo} from '../../models/HeaderInfo';
import {Router} from '@angular/router';
import {Reservations} from '../../models/Reservation';
import {Page} from '../../models/PaginatedResponse';



@Component({
  selector: 'app-list-reservation',
  templateUrl: './list-reservation.component.html',
  styleUrls: ['./list-reservation.component.scss'],

})
export class ListReservationComponent implements OnInit {

  public reservations: Reservations[];
  public page: Page;
  icon: string;
  iconBlack: string;
  iconRed: string;
  ratingCount: number;
  currentRate: number;
  currentPage: number;
  baseUrl: string;
  pages: number[];
  header: HeaderInfo = new HeaderInfo();
  user: Users;
  headerLink = 'Create Reservation';
  selectIndex = 0 ;


  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string, public router: Router) {
    this.baseUrl = baseUrl;
    http.get(baseUrl + 'reservations').subscribe((result: Page) => {
      this.page = result;
      // @ts-ignore
      this.reservations = result.outcome;
      this.currentPage = this.page.currentPage;
      this.updatePages(this.page.totalPages > 1 ? this.page.totalPages : 1);
      this.user = new Users();
      console.log(result);
      console.log(this.reservations);
      this.updateHeaderInfo();
    }, error => console.error(error));
  }

  ngOnInit(): void {
    this.iconBlack = '../../assets/favorite_icon.svg';
    this.iconRed = '../../assets/favorite_icon_red.svg';
    this.icon = this.iconBlack;
    this.ratingCount = 5;
    this.currentRate = 3.5;
    this.currentPage = 1;
    this.updatePages(1);
    this.updateHeaderInfo();
    this.headerLink = 'Create Reservation';
  }

  addFav(fav?: boolean, reservId?: string): void {
    fav = ! fav;
    this.icon = fav ? this.iconRed : this.iconBlack;

    this.http.put(this.baseUrl + 'reservations/favorite?currentPage=' + this.currentPage + '&orderByCriteria=' + this.selectIndex,
      {IsFav: fav, Id: reservId}).subscribe((result: Page) => {
      this.page = result;
      // @ts-ignore
      this.reservations = result.outcome;
      this.currentPage = this.page.currentPage;
      this.updatePages(this.page.totalPages > 1 ? this.page.totalPages : 1);
    }, error => console.error(error));
  }

  edit(id: string): void {
    console.log(id);
    this.updateHeaderInfo2();
    this.router.navigate(['/form/' + id]);
  }
  updateRating(rate?: number, reserveId?: string): void {
    this.http.put<Reservations[]>(this.baseUrl + 'reservations/rate/' + reserveId +
      '?currentPage=' + this.currentPage + '&orderByCriteria=' + this.selectIndex, {Rate: rate}).subscribe(result => {
    }, error => {
      console.error(error);
    });
  }
  updatePages(pages): void {
    this.pages = newArray(pages);
  }

  getRecords(page): void {
    if (page > this.page.totalPages){
      page = this.page.totalPages;
    }
    else if (page < 1) {
      page = 1;
    }

    console.log('pagina' + page);
    // @ts-ignore
    this.http.get(this.baseUrl + 'reservations', {params: {currentPage: page,
        totalRecordsByPage: '10', orderByCriteria: this.selectIndex.toString()}}).subscribe((result: Page) => {
      this.page = result;
      // @ts-ignore
      this.reservations = result.outcome;
      this.currentPage = this.page.currentPage;
      this.updatePages(this.page.totalPages > 1 ? this.page.totalPages : 1);
      this.user = new Users();
      this.updateHeaderInfo();
    }, error => console.error(error));
    this.currentPage = page;
  }

  updateHeaderInfo(): void {
    this.header.title = 'Reservation List';
    this.header.btnTitle = 'Create Reservation';
    this.header.text = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Beatae consequuntur eaque eligendi eos est eveniet,\n' +
      '      harum in, libero nisi qui quia ratione rem suscipit ullam vel velit veritatis. Consequuntur, sed.';
    this.header.function = ['/form'];
  }
  updateHeaderInfo2(): void {
    this.header.title = 'Create Reservation';
    this.header.btnTitle = null;
    this.header.text = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Beatae consequuntur eaque eligendi eos est eveniet,\n' +
      '      harum in, libero nisi qui quia ratione rem suscipit ullam vel velit veritatis. Consequuntur, sed.';
    this.header.function = ['/'];
  }

  selectionChanged($event?): void {
    this.selectIndex = $event.target.selectedIndex;
    this.http.get(this.baseUrl + 'reservations',{params: {currentPage: this.currentPage.toString(),
        totalRecordsByPage: '10', orderByCriteria: this.selectIndex.toString()}}).subscribe((result: Page) => {
      this.page = result;
      // @ts-ignore
      this.reservations = result.outcome;
      this.currentPage = this.page.currentPage;
      this.updatePages(this.page.totalPages > 1 ? this.page.totalPages : 1);
      this.user = new Users();
      this.updateHeaderInfo();
    }, error => console.error(error));
  }
}
