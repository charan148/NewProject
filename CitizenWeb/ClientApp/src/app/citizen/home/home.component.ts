import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { fuseAnimations } from '@fuse/animations';

import { CitizenService } from 'app/citizen/citizen.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
    animations : fuseAnimations
})
export class HomeComponent implements OnInit, OnDestroy
{
    reports: any = [];
    categories: any[];
    courses: any[];
    coursesFilteredByCategory: any[];
    filteredCourses: any[];
    currentCategory: string;
    searchTerm: string;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {CitizenService} _citizenService
     */
    constructor(
        private _citizenService: CitizenService
    )
    {
        // Set the defaults
        this.currentCategory = 'all';
        this.searchTerm = '';

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    
    ngOnInit(): void
    {
        
        

        this.reports = [];
        this.reports.push(
            { reportId: 2, reportName: 'Request a Garbage Cart, Recycling Cart or Composter', issue: 'Request a new or replacement garbage or recycling cart for your home.' },
            { reportId: 3, reportName: 'Report a Missed Garbage Cart Pick-Up', issue: 'Let us know what dumpster was missed so we can pick it up.' },
            { reportid: 7, reportName: 'Report a Missed Garbage Dumpster Pick-Up', issue: 'Let us know if your commercial garbage dumpster from your business or apartment was missed so we can pick it up.' },
            { reportId: 8, reportName: 'Request to Reduce Garbage Service for Your Business', issue: 'The City of Orlando is offering a reduction in the level of garbage service to businesses that have been impacted or have had to temporarily close due to COVID-19.' },
            { reportId: 9, reportName: 'Report a Missed Commercial Food Waste Pick-Up', issue: 'Let us know if we missed your food waste cart so we can pick it up.' },
            { reportId: 1, reportName: 'Report a Broken or Missing Traffic Sign', issue: 'The City of Orlando performs sign repairs and replacements for the safety of all drivers and pedestrians.' },
            { reportId: 4, reportName: 'Report a Something Broken in a City Park', issue: 'The City of Orlando responds to repair requests to keep all parks beautiful and safe for all visitors.' },
            { reportId: 5, reportName: 'Report a Code Violation Inside Your Residence', issue: 'Report a Code Violation Inside Your Residence' },
            { reportId: 6, reportName: 'Report a Dangerous or Rundown Housing', issue: 'Examples of dangerous or rundown lots include excessive garbage, stagnant water, fence issues or unsanitary conditions.' })
        // Subscribe to categories
        this._citizenService.onCategoriesChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(categories => {
                this.categories = categories;
            });
        this.coursesFilteredByCategory = this.reports;
        //// Subscribe to courses
        //this._academyCoursesService.onCoursesChanged
        //    .pipe(takeUntil(this._unsubscribeAll))
        //    .subscribe(courses => {
               
        //        this.filteredCourses = this.coursesFilteredByCategory = this.courses = courses;
        //    });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

  
    /**
     * Filter courses by term
     */
    filterCoursesByTerm(): void
    {
        const searchTerm = this.searchTerm.toLowerCase();

        // Search
        if ( searchTerm === '' )
        {
            this.reports = this.coursesFilteredByCategory;
        }
        else
        {
            this.reports = this.coursesFilteredByCategory.filter((course) => {
                return course.reportName.toLowerCase().includes(searchTerm);
            });
        }
    }
    report(name: any) {
        sessionStorage.setItem('reportName', name);
    }
}
