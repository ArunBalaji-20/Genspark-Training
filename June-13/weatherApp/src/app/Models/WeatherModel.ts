
// export class WeatherModel{
//     constructor(public cityname:string="",public temperature:string="",public description:string="")
//     {

//     }
// }
export class WeatherModel {
    constructor(
        public cityname: string = "",
        public temperature: number = 0,
        public description: string = ""
    ) {}

    static fromApiResponse(api: any): WeatherModel {
        return new WeatherModel(
            api.name || "",
            api.main?.temp || 0,
            api.weather?.[0]?.description || ""
        );
    }
}