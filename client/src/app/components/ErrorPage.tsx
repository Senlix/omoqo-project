import React from 'react';
import { Flex, Typography } from 'antd';

const { Text, Link, Title } = Typography;

const ErrorPage: React.FC = () => {
  return (
    <Flex justify={'center'} align={'center'} vertical>
      <Title level={2}>Ops...</Title>
      <Text>Something went wrong. Please try again later.</Text>
    </Flex>
  );
};

export default ErrorPage;
