import { useEffect } from 'react';
import type { FilterValue, SorterResult } from 'antd/es/table/interface';
import shipService, { Ship } from '../../../services/shipService';
import { AxiosRequestConfig } from 'axios';
import { shipTableStore } from './shipTableStore';

const useShipTableData = () => {
  const shipTableData = shipTableStore();

  const prepareSorter = (
    sort: SorterResult<Ship> | SorterResult<Ship>[]
  ): string => {
    let sorter = '';

    if (Array.isArray(sort)) {
      sorter = sort.map((item) => `${item.field}:${item.order}`).join(', ');
    } else if (sort.field && sort.order) {
      sorter = `${sort.field}:${sort.order}`;
    }

    return sorter;
  };

  const prepareFilter = (filters: Record<string, FilterValue | null>) => {
    let filter: any = {};

    Object.entries(filters).forEach(([key, value]) => {
      if (value !== null) {
        const valueString = Array.isArray(value)
          ? value.join(', ')
          : String(value);
        filter[key] = valueString.trim();
      }
    });

    return filter;
  };

  useEffect(() => {
    shipTableData.setLoading(true);

    const { filters, pagination, sorter } = shipTableData.tableParams;

    const requestConfig: AxiosRequestConfig = {
      params: {
        page: pagination?.current,
        limit: pagination?.pageSize,
      },
    };

    if (filters) {
      const filter = prepareFilter(filters);
      requestConfig.params = { ...requestConfig.params, ...filter };
    }

    if (sorter) {
      const orderBy = prepareSorter(sorter);
      requestConfig.params.orderBy = orderBy;
    }

    const { request, cancel } = shipService.getAll<Ship>(requestConfig);

    request
      .then(({ data: result }) => {
        shipTableData.setData(result.items);
        shipTableData.setTableParams({
          ...shipTableData.tableParams,
          pagination: {
            ...shipTableData.tableParams.pagination,
            total: result.count,
          },
        });
      })
      .catch((err) => {
        
      })
      .finally(() => {
        shipTableData.setRefresh(false);
        shipTableData.setLoading(false);
      });

    return () => cancel();
  }, [
    shipTableData.tableParams.pagination?.current,
    shipTableData.tableParams.pagination?.pageSize,
    shipTableData.tableParams.sorter,
    shipTableData.refresh == true,
  ]);
};

export default useShipTableData;
