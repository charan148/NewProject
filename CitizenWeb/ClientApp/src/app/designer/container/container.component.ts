import { Component, Input } from "@angular/core";

@Component({
    selector: 'container',
    templateUrl: './container.component.html',
})
export class ContainerComponent {
    @Input() model: { type: string, id: number, value: string, columns };
    @Input() model1: { type: string, id: number, value: string, column1 };
    @Input() models: { type: string, id: number, value: string, columns, column1 };


    @Input() list: any[];

    public isArray(object): boolean {
        return Array.isArray(object);
    }

    public removeItem(item: any, list: any[]): void {
        list.splice(list.indexOf(item), 1);
    }
    public  rowDelete(item: any, list: any) {        
        let index = list.indexOf(item);      
        list.splice(index, 1);
    }
}