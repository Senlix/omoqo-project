import { InputRef, Input, Space, Button } from 'antd';
import { ColumnType } from 'antd/es/table';
import { useRef } from 'react';
import { SearchOutlined } from '@ant-design/icons';
import { Ship } from 'src/app/services/shipService';
import styles from './shipList.module.scss';

const useShipTableFilters = () => {
  const searchInput = useRef<InputRef>(null);

  const getColumnSearchProps = (dataIndex: keyof Ship): ColumnType<Ship> => ({
    filterDropdown: ({
      setSelectedKeys,
      selectedKeys,
      confirm,
      clearFilters,
      close,
    }) => (
      <div className={styles.filterContainer} onKeyDown={(e) => e.stopPropagation()}>
        <Input
          ref={searchInput}
          placeholder={`Search ${dataIndex}`}
          value={selectedKeys[0]}
          onChange={(e) =>
            setSelectedKeys(e.target.value ? [e.target.value] : [])
          }
          onPressEnter={() => confirm()}
          className={styles.filterInput}
        />
        <Space>
          <Button
            type="primary"
            onClick={() => confirm({ closeDropdown: false })}
            icon={<SearchOutlined />}
            size="small"
            className={styles.filterButton}
          >
            Search
          </Button>
          <Button
            onClick={() => clearFilters!()}
            size="small"
            className={styles.filterButton}
          >
            Reset
          </Button>
          <Button
            type="link"
            size="small"
            onClick={() => {
              close();
            }}
          >
            close
          </Button>
        </Space>
      </div>
    ),
    filterIcon: (filtered: boolean) => (
      <SearchOutlined style={{ color: filtered ? '#1677ff' : undefined }} />
    ),
    onFilterDropdownOpenChange: (visible) => {
      if (visible) {
        setTimeout(() => searchInput.current?.select(), 100);
      }
    },
    render: (text) => text,
  });

  return { getColumnSearchProps };
};

export default useShipTableFilters;
