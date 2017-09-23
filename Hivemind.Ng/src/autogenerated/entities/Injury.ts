
export class Injury {
    public injuryEnum: string;
    public name: string;
    public description: string;
    public injuryEffect: any;

    public constructor(partial: Partial<Injury>) {
        if (partial.injuryEnum) {
            this.injuryEnum = partial.injuryEnum;
        }
        if (partial.name) {
            this.name = partial.name;
        }
        if (partial.description) {
            this.description = partial.description;
        }
        if (partial.injuryEffect) {
            this.injuryEffect = partial.injuryEffect;
        }
    }
}