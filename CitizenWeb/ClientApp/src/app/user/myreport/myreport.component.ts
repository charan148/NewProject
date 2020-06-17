import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, Observable } from 'rxjs';
import * as shape from 'd3-shape';
import { fuseAnimations } from '@fuse/animations';
import { MyReportService } from 'app/user/myreport/myreport.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { FormControl } from '@angular/forms';
import * as moment from 'moment';
interface Time {
    value: number;
    viewValue: string;
}

@Component({
    selector: 'myreport',
    templateUrl: './myreport.component.html',
    styleUrls: ['./myreport.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class MyReportComponent implements OnInit {
    projects: any[];
    selectedProject: any;
    selectedTime: any;
    effectiveToDate: any;
    effectiveFromDate: any;
    timeChange: any;
    widgets: any;
    widget5: any = {};
    widget12: any = {};
    widget7: any = {};
    widget8: any = {};
    widget9: any = {};
    widget11: any = {};
    selected: any = 1;
    currentRange: any = 'TW'
    dateNow = Date.now();
    date = new FormControl(new Date());
    times: Time[] = [
        { value: 1, viewValue: 'This Week' },
        { value: 2, viewValue: 'This Month' },
        { value: 3, viewValue: 'This Quarter' },
        { value: 4, viewValue: 'This Year' },
        { value: 5, viewValue: 'Last Week' },
        { value: 6, viewValue: 'Last Month' },
        { value: 7, viewValue: 'Last Quarter' },
        { value: 8, viewValue: 'Last Year' },
        { value: 9, viewValue: 'First Quarter' },
        { value: 10, viewValue: 'Second Quarter' },
        { value: 11, viewValue: 'Third Quarter' },
        { value: 12, viewValue: 'Fourth Quarter' }
    ];
    minDate: Date;
    maxDate: Date;
    /**
     * Constructor
     *
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {MyReportService} _myReportService
     */
    constructor(
        private _fuseSidebarService: FuseSidebarService,
        private _myReportService: MyReportService
    ) {

        /**
         * Widget 12
         */
        this.widget12 = {
            currentRange: 'TW',
            legend: false,
            explodeSlices: false,
            labels: true,
            doughnut: true,
            gradient: false,
            scheme: {
                domain: ['#f44336', '#9c27b0', '#03a9f4', '#e91e63', '#C7B42C', '#ffc107']
            },
            onSelect: (ev) => {
            }
        };

        

        setInterval(() => {
            this.dateNow = Date.now();
        }, 1000);

    }
    startDateChange(val) {
        let a = val;

    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        var now = new Date; // get current date
        var first = now.getDate() - now.getDay() + 1;
        var last = first + 6; // last day is the first day + 6
        var month = now.getMonth() + 1;
        var year = now.getFullYear().toString().substr(-2);
        var firstdate = month.toString() + "/" + first.toString() + "/" + year;
        var lastdate = month.toString() + "/" + last.toString() + "/" + year;
        let effectivefromDate = new Date(firstdate);
        let effectivetoDate = new Date(lastdate);
        this.effectiveFromDate = moment(effectivefromDate);
        this.effectiveToDate = moment(effectivetoDate);
        this.timeChange = this.times[0].value;
        this.projects = this._myReportService.projects;
        this.selectedProject = this.projects[0];
        this.selectedTime = this.times[0];
        this.widgets = this._myReportService.widgets;
        const currentYear = new Date().getFullYear();
        this.minDate = new Date(currentYear - 20, 0, 1);
        this.maxDate = new Date(currentYear + 1, 11, 31);

        
        var now = new Date();
        var quarter = Math.floor((now.getMonth() / 3));
        var firstDate = new Date(now.getFullYear(), quarter * 3, 1);
        var endDate = new Date(firstDate.getFullYear(), firstDate.getMonth() + 3, 0);
        
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    timeChangeM() {
        let now = new Date;
        if (this.timeChange == 1) {
            this.currentRange = 'TW'
            //var now = new Date; // get current date
            var first = now.getDate() - now.getDay() + 1;
            var last = first + 6; // last day is the first day + 6
            var month = now.getMonth() + 1;
            var year = now.getFullYear().toString().substr(-2);
            var firstdate = month.toString() + "/" + first.toString() + "/" + year;
            var lastdate = month.toString() + "/" + last.toString() + "/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 2) {
            this.currentRange = 'TM'
            var month = now.getMonth() + 1;
            var year = now.getFullYear().toString().substr(-2);
            var lastDay = new Date(now.getFullYear(), now.getMonth() + 1, 0);
            var last = lastDay.getDate();
            var firstdate = month.toString() + "/1/" + year;
            var lastdate = month.toString() + "/" + last.toString() + "/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 3) {
            this.currentRange = 'TQ'
            var year = now.getFullYear().toString().substr(-2);
            var quarter = Math.floor((now.getMonth() / 3));
            var firstDate = new Date(now.getFullYear(), quarter * 3, 1);
            var endDate = new Date(firstDate.getFullYear(), firstDate.getMonth() + 3, 0);
            var firstmonth = firstDate.getMonth() + 1;
            var lastmonth = endDate.getMonth() + 1;
            var firstdate = firstmonth.toString() + "/1/" + year;
            var lastdate = lastmonth.toString() + "/" + endDate.getDate().toString() + "/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 4) {
            this.currentRange = 'TY'
            var year = now.getFullYear().toString().substr(-2);
            var firstdate = "01/01/" + year;
            var lastdate = "12/31/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 5) {
            this.currentRange = 'LW'
            var beforeOneWeek = new Date(new Date().getTime() - 60 * 60 * 24 * 7 * 1000)
            var first = beforeOneWeek.getDate() - beforeOneWeek.getDay() + 1;
            var last = first + 6;
            var month = beforeOneWeek.getMonth() + 1;
            var year = beforeOneWeek.getFullYear().toString().substr(-2);
            var firstdate = month.toString() + "/" + first.toString() + "/" + year;
            var lastdate = month.toString() + "/" + last.toString() + "/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 6) {
            this.currentRange = 'LM'
            var month = now.getMonth();
            var year = now.getFullYear().toString().substr(-2);
            var lastDay = new Date(now.getFullYear(), now.getMonth(), 0);
            var last = lastDay.getDate();
            var firstdate = month.toString() + "/1/" + year;
            var lastdate = month.toString() + "/" + last.toString() + "/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 7) {
            this.currentRange = 'LQ'
            var year = now.getFullYear().toString().substr(-2);
            var quarter = Math.floor((now.getMonth() / 3));
            var firstDate = new Date(now.getFullYear(), quarter * 3 - 3, 1);
            var endDate = new Date(firstDate.getFullYear(), firstDate.getMonth() + 3, 0);
            var firstmonth = firstDate.getMonth() + 1;
            var lastmonth = endDate.getMonth() + 1;
            var firstdate = firstmonth.toString() + "/1/" + year;
            var lastdate = lastmonth.toString() + "/" + endDate.getDate().toString() + "/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 8) {
            this.currentRange = 'LY'
            var year = (now.getFullYear() - 1).toString().substr(-2);
            var firstdate = "01/01/" + year;
            var lastdate = "12/31/" + year;
            let effectivefromDate = new Date(firstdate);
            let effectivetoDate = new Date(lastdate);
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 9) {
            this.currentRange = '1Q'
            let effectivefromDate = new Date("01/01/20");
            let effectivetoDate = new Date("03/31/20");
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 10) {
            this.currentRange = '2Q'
            let effectivefromDate = new Date("04/01/20");
            let effectivetoDate = new Date("06/30/20");
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 11) {
            this.currentRange = '3Q'
            let effectivefromDate = new Date("07/01/20");
            let effectivetoDate = new Date("09/30/20");
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        } else if (this.timeChange == 12) {
            this.currentRange = '4Q'
            let effectivefromDate = new Date("10/01/20");
            let effectivetoDate = new Date("12/31/20");
            this.effectiveFromDate = moment(effectivefromDate);
            this.effectiveToDate = moment(effectivetoDate);
        }
    }
    /**
     * Toggle the sidebar
     *
     * @param name
     */
    toggleSidebar(name): void {
        this._fuseSidebarService.getSidebar(name).toggleOpen();
    }
}

export class FilesDataSource extends DataSource<any>
{
    /**
     * Constructor
     *
     * @param _widget11
     */
    constructor(private _widget11) {
        super();
    }

    /**
     * Connect function called by the table to retrieve one stream containing the data to render.
     *
     * @returns {Observable<any[]>}
     */
    connect(): Observable<any[]> {
        return this._widget11.onContactsChanged;
    }

    /**
     * Disconnect
     */
    disconnect(): void {
    }
}

