import React from 'react';
import { Button, Flex } from 'antd';
import Title from 'antd/es/typography/Title';

import { shipTableRowSelectionStore, shipTableStore } from './shipTableStore';
import styles from './shipList.module.scss';
import { useNavigate } from 'react-router-dom';
import shipService from '../../../services/shipService';

const ShipListHeader: React.FC = () => {
  const shipTableRowSelection = shipTableRowSelectionStore();
  const shipTableData = shipTableStore();

  const navigate = useNavigate();

  const hasSelected = shipTableRowSelection.selectedRowKeys.length > 0;

  const removeShip = () => {
    shipTableData.setLoading(true);
    const { request } = shipService.bulkRemove({
      data: {
        ids: shipTableRowSelection.selectedRowKeys,
      },
    });

    request
      .then(({}) => {
        shipTableData.setRefresh(true);
        shipTableRowSelection.onChange([]);
      })
      .catch((err) => {})
      .finally(() => shipTableData.setLoading(false));
  };

  return (
    <>
      <Flex justify={'space-between'} align={'center'}>
        <Title level={2}>Ship List</Title>
        <div>
          <Button
            onClick={removeShip}
            className={styles.deleteButton}
            type="primary"
            danger
            disabled={!hasSelected}
            loading={shipTableData.isLoading}
          >
            Delete
          </Button>
          <Button onClick={() => navigate('/ships/new')} type="primary">
            New Record
          </Button>
        </div>
      </Flex>
    </>
  );
};

export default ShipListHeader;
