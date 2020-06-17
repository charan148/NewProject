import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.scss']
})
export class LocationComponent implements OnInit {
    locations = [];
    location: string = '';
    locationchk: boolean;
    reportExist: boolean;
    fallowbtn: boolean; reportName: string = '';
    discription = '';
    previewUrl: any = '';
    garbagecheck: boolean;
    recycle: boolean;
    yardwaste: boolean;
    register = true;
    submitself = 'Submit as yourself';
    submitAnony = 'Submit Anonymously';
    btnName = 'Continue';
    submitstepperhead = 'Submit';
    tracktepperhead = 'Submit';
    isRegButton: boolean;
    isHideTab: boolean;
    isSub: boolean;
    isReg: boolean;
    loginsuc: boolean;
    isForgot: boolean;
    isSignIn: boolean;
    
    @Output() confirmLocationClick = new EventEmitter();
    @Output() locationSearchs = new EventEmitter();
    @ViewChild('browse', { static: false }) private browseref: ElementRef;
    constructor(private _domSanitizer: DomSanitizer) { }
    locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3504.9543538533944!2d-81.38068808505365!3d28.541091694964773!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e77afe775c318f%3A0xfe725ffe0a44fc34!2s121%20S%20Orange%20Ave%2C%20Orlando%2C%20FL%2032801%2C%20USA!5e0!3m2!1sen!2sin!4v1589274503328!5m2!1sen!2sin";
    trustedLocation: any;
    ngOnInit(): void {
        this.methodToGetURL();
  }
    locationSearch(val: any) {

        this.locations = [

            { locationId: 2, name: '1022 Ridgecrest Rd, Orlando, FL 32806, USA', latitude: '28.504566', longitude: '-81.364743' },
            { locationId: 3, name: '8767 Coco Plum Pl ,Orlando, FL 32827, USA', latitude: '28.429139', longitude: '-81.263757' },
            { locationId: 4, name: '4108 Kalwit Ln, Orlando, FL 32808, USA', latitude: '28.561311', longitude: '-81.428886' },
            { locationId: 5, name: '3210 N Orange Ave, Orlando, FL 32803, USA', latitude: '28.579755', longitude: '-81.372690' },
            { locationId: 6, name: '743 N Magnolia Ave, Orlando, FL 32803, USA', latitude: '28.555050', longitude: '-81.376653' },
            { locationId: 1, name: '121 S Orange Ave, Orlando, FL 32801, USA', latitude: '28.541228', longitude: '-81.378521' },
            { locationId: 7, name: '1511 E State Road 434, Ste. 3033, Winter Springs, FL 32708, USA', latitude: '28.689869', longitude: '-81.232209' }
        ];

        this.locations = this.locations.filter(tt => tt.name.includes(val));
        this.location = val;
        if (val == '') {
            this.locations = [];
        }

    }
    
    methodToGetURL() {
        this.trustedLocation = this._domSanitizer.bypassSecurityTrustResourceUrl(this.locationSrc);
    }
    optionSelected(val: any) {
        this.location = val;


        if (this.location == '1511 E State Road 434, Ste. 3033, Winter Springs, FL 32708, USA') {
            this.locationchk = true;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3499.997456608865!2d-81.23436548505072!3d28.689722688241307!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e76beae436b827%3A0x9d1787c7debb306!2s1511%20E%20State%20Rd%20434%20%233033%2C%20Winter%20Springs%2C%20FL%2032708%2C%20USA!5e0!3m2!1sen!2sin!4v1589452505818!5m2!1sen!2sin";
            this.locations = [];
        } else if (this.location == '121 S Orange Ave, Orlando, FL 32801, USA') {
            this.locationchk = false;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3504.9543538533944!2d-81.38068808505365!3d28.541091694964773!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e77afe775c318f%3A0xfe725ffe0a44fc34!2s121%20S%20Orange%20Ave%2C%20Orlando%2C%20FL%2032801%2C%20USA!5e0!3m2!1sen!2sin!4v1589274503328!5m2!1sen!2sin";
        } else if (this.location == '1022 Ridgecrest Rd, Orlando, FL 32806, USA') {
            this.locationchk = false;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3506.175612859312!2d-81.36692118505437!3d28.50436369662127!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e77b68898e4e37%3A0x592aa8fd64a69ef6!2s1022%20Ridgecrest%20Rd%2C%20Orlando%2C%20FL%2032806%2C%20USA!5e0!3m2!1sen!2sin!4v1590039996865!5m2!1sen!2sin";
        } else if (this.location == '8767 Coco Plum Pl ,Orlando, FL 32827, USA') {
            this.locationchk = false;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3508.6788529937344!2d-81.2660095850558!3d28.42894570001711!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e762303deb4959%3A0xdc2e17ddcb147a1c!2s8767%20Coco%20Plum%20Pl%2C%20Orlando%2C%20FL%2032827%2C%20USA!5e0!3m2!1sen!2sin!4v1590040063528!5m2!1sen!2sin";
        } else if (this.location == '4108 Kalwit Ln, Orlando, FL 32808, USA') {
            this.locationchk = false;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3504.291915355299!2d-81.43105278505323!3d28.560995694066126!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e7798a6cb30aa5%3A0x6333c39a5ac7555a!2s4108%20Kalwit%20Ln%2C%20Orlando%2C%20FL%2032808%2C%20USA!5e0!3m2!1sen!2sin!4v1590040125025!5m2!1sen!2sin";
        } else if (this.location == '3210 N Orange Ave, Orlando, FL 32803, USA') {
            this.locationchk = false;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3503.6739496557893!2d-81.37488958505287!3d28.57955199322791!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e77a9cac698017%3A0xb430012212eb406d!2s3210%20N%20Orange%20Ave%2C%20Orlando%2C%20FL%2032803%2C%20USA!5e0!3m2!1sen!2sin!4v1590040193025!5m2!1sen!2sin";
        } else if (this.location == '743 N Magnolia Ave, Orlando, FL 32803, USA') {
            this.locationchk = false;
            this.locationSrc = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3504.4978477622526!2d-81.37886278505331!3d28.554809494345523!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88e77af1134d9e23%3A0x84d90f86355dfe71!2s743%20N%20Magnolia%20Ave%2C%20Orlando%2C%20FL%2032803%2C%20USA!5e0!3m2!1sen!2sin!4v1590040254357!5m2!1sen!2sin";
        }


        this.methodToGetURL();
        this.locationSearchs.emit(val);
    }



   
    update() {
        this.location = '';
        this.locationchk = false;
        this.locations = [];
    }
    
    confirmLoaction() {

        if (this.location != '121 S Orange Ave, Orlando, FL 32801, USA' && this.location != '1022 Ridgecrest Rd, Orlando, FL 32806, USA' && this.location != '8767 Coco Plum Pl ,Orlando, FL 32827, USA' && this.location != '4108 Kalwit Ln, Orlando, FL 32808, USA' && this.location != '3210 N Orange Ave, Orlando, FL 32803, USA' && this.location != '743 N Magnolia Ave, Orlando, FL 32803, USA') {
            this.locationchk = true;
        } else {
            const locationObj = this.locations.filter(tt => tt.name == this.location)[0];
            this.confirmLocationClick.emit(locationObj);
          //  this.locations = [];
            this.locationchk = false;


        }
    }
}
