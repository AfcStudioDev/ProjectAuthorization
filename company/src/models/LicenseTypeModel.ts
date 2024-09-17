export class LicenseTypeModel {
    constructor(id = "", name = "", price = 0, duration = 0) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.duration = duration
    }
    id: string
    name: string
    price: number
    duration: number
}