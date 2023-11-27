import create from './httpService';

export interface Ship {
  id: string;
  code: string;
  name: string;
  length: number;
  width: number;
}

export default create("/Ships");
