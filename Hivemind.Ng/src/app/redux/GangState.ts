import { Ganger } from '../entities/Ganger';
import { Gang } from '../entities/Gang';
import { GangHouse } from '../entities/GangHouse';
import { AnyAction, Reducer } from 'redux';
import { IAppState } from './IAppState';

// action type constants
export const CHANGE_GANG: string = 'GangState.CHANGE_GANG';
export const ADD_GANG: string = 'GangState.ADD_GANG';
export const ADD_GANGER: string = 'GangState.ADD_GANGER';
export const EDIT_GANG: string = 'GangState.EDIT_GANG';
export const EDIT_GANGER: string = 'GangState.EDIT_GANGER';
export const BUY_WEAPON: string = 'GangState.BUY_WEAPON';
export const EQUIP_WEAPON: string = 'GangState.EQUIP_WEAPON';

var initialGang = new Gang();
initialGang.gangHouse = 'Cawdor';
initialGang.gangers = []; 

var initialState: IAppState = {
    gang: initialGang
};

export const reduce: Reducer<IAppState> = (state: IAppState = initialState, action: AnyAction): IAppState => {
    let newState: IAppState;

    switch (action.type) {
        case CHANGE_GANG:
            if (action.payload !instanceof Gang) {
                return state;
            } 
            newState = Object.assign({}, state, { gang: action.payload });
            console.log(newState);
            return newState;

        case ADD_GANG:
            if (action.payload !instanceof Gang) {
                return state;
            } 
            newState = Object.assign({}, state, { gang: action.payload });
            return newState;

        case ADD_GANGER:
            if (action.payload !instanceof Ganger) {
                return state;
            }
            newState = Object.assign({}, state, { gang: state.gang });
            newState.gang.gangers.push(action.payload); 
            return newState; 

        default:
            return state;
    }
}
