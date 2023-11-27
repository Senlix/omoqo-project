import { Table } from 'antd';
import useShipTableFilters from './useShipTableFilters';
import useShipTableData from './useShipTableData';
import { ColumnsType } from 'antd/es/table/interface';
import { Ship } from 'src/app/services/shipService';
import { shipTableRowSelectionStore, shipTableStore } from './shipTableStore';
import { Link, useNavigate } from 'react-router-dom';
import styles from './shipList.module.scss';

const ShipTable: React.FC = () => {
  useShipTableData();

  const shipTableData = shipTableStore();
  const shipListRowSelection = shipTableRowSelectionStore();

  const { getColumnSearchProps } = useShipTableFilters();

  const navigate = useNavigate();

  const columns: ColumnsType<Ship> = [
    {
      title: 'Code',
      dataIndex: 'code',
      ...getColumnSearchProps('code'),
      sorter: {
        multiple: 1,
      },
      width: '10%',
    },
    {
      title: 'Name',
      dataIndex: 'name',
      ...getColumnSearchProps('name'),
      sorter: {
        multiple: 2,
      },
      width: '70%',
      render: (text, record) => <Link to={`/ships/${record.id}`}>{text}</Link>,
    },
    {
      title: 'Width',
      dataIndex: 'width',
      sorter: {
        multiple: 3,
      },
      width: '10%',
    },
    {
      title: 'Length',
      dataIndex: 'length',
      sorter: {
        multiple: 4,
      },
      width: '10%',
    },
  ];

  const hasSelected = shipListRowSelection.selectedRowKeys.length > 0;

  const onShipTableRow = (record: Ship, rowIndex?: number) => ({
    onDoubleClick: (event: React.MouseEvent) => {
      navigate(`/ships/${record.id}`);
    },
  });

  const renderFooter = () =>
    hasSelected
      ? `Selected ${shipListRowSelection.selectedRowKeys.length} items`
      : 'No item selected';

  return (
    <Table
      rowKey={(record) => record.id}
      rowClassName={styles.tableRow}
      dataSource={shipTableData.data}
      columns={columns}
      pagination={shipTableData.tableParams.pagination}
      onRow={onShipTableRow}
      onChange={shipTableData.handleTableChange}
      rowSelection={shipListRowSelection}
      scroll={{ y: 'calc(100vh - 320px)' }}
      loading={shipTableData.isLoading}
      size="small"
      bordered
      footer={renderFooter}
    />
  );
};

export default ShipTable;
