export class GameData {
    public gangerId: string;
    public value: any;

    constructor(partial: Partial<GameData>) {
        if (partial.gangerId) {
            this.gangerId = partial.gangerId;
        }

        if (partial.value) {
            this.value = partial.value;
        }
    }
}
