import { create } from 'zustand';
import { Ship } from '../../../services/shipService';
import { TablePaginationConfig } from 'antd';
import { FilterValue, SorterResult } from 'antd/es/table/interface';

export interface ShipTableParams {
  pagination?: TablePaginationConfig;
  filters?: Record<string, FilterValue | null>;
  sorter?: SorterResult<Ship> | SorterResult<Ship>[];
}

export interface ShipTableStore {
  refresh: boolean;
  data: Ship[];
  isLoading: boolean;
  tableParams: ShipTableParams;

  setRefresh: (refresh: boolean) => void;
  setLoading: (loading: boolean) => void;
  setData: (data: any) => void;
  setTableParams: (tableParams: ShipTableParams) => void;
  handleTableChange: (
    pagination: TablePaginationConfig,
    filters: Record<string, FilterValue | null>,
    sorter: SorterResult<Ship> | SorterResult<Ship>[]
  ) => void;
}

const shipTableStore = create<ShipTableStore>((set) => ({
  refresh: false,
  data: [],
  isLoading: false,
  tableParams: {
    pagination: {
      current: 1,
      pageSize: 20,
    },
  },
  setRefresh: (refresh: boolean) => {
    set(() => ({ refresh: refresh }));
  },
  setLoading: (loading: boolean) => {
    set(() => ({ isLoading: loading }));
  },
  setData: (data: Ship[]) => {
    set(() => ({ data }));
  },
  setTableParams: (tableParams: ShipTableParams) => {
    set(() => ({ tableParams }));
  },
  handleTableChange: (
    pagination: TablePaginationConfig,
    filters: Record<string, FilterValue | null>,
    sorter: SorterResult<Ship> | SorterResult<Ship>[]
  ) => {
    set(() => ({
      tableParams: {
        pagination,
        filters,
        sorter,
      },
    }));
  },
}));

export interface ShipTableRowSelectionStore {
  selectedRowKeys: React.Key[];
  onChange: (newSelectedRowKeys: React.Key[]) => void;
}

const shipTableRowSelectionStore = create<ShipTableRowSelectionStore>(
  (set) => ({
    selectedRowKeys: [],
    onChange: (newSelectedRowKeys: React.Key[]) => {
      set(() => ({ selectedRowKeys: newSelectedRowKeys }));
    },
  })
);

export { shipTableRowSelectionStore, shipTableStore };
