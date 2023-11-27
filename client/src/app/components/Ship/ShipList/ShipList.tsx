import React from 'react';

import ShipTable from './ShipTable';
import ShipListHeader from './ShipListHeader';

const ShipList: React.FC = () => {
  return (
    <>
      <ShipListHeader />
      <ShipTable />
    </>
  );
};

export default ShipList;
