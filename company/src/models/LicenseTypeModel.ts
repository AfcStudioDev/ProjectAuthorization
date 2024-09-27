export class LicenseTypeModel {
    constructor(id: number = 0, name = "", price = 0, duration = 0) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.duration = duration
    }
    id: number
    name: string
    price: number
    duration: number
}