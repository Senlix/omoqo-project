import React, { useEffect } from 'react';
import Title from 'antd/es/typography/Title';
import { Button, Card, Checkbox, Flex, Form, Input, InputNumber } from 'antd';
import shipService, { Ship } from '../../../services/shipService';
import { useNavigate, useParams } from 'react-router-dom';

import styles from './shipList.module.scss';
import { MaskedInput } from 'antd-mask-input';

type FieldType = {
  code?: string;
  name?: string;
  width?: number;
  length?: number;
};

const ShipForm: React.FC = () => {
  const params = useParams();
  const navigate = useNavigate();

  const [form] = Form.useForm();

  let id = 0;

  if (params.id) {
    id = parseInt(params.id);

    if (isNaN(id)) {
      navigate('/');
    }
  }

  useEffect(() => {
    const { request, cancel } = shipService.getById<Ship>(id);

    request
      .then(({ data: result }) => {
        form.setFieldsValue(result);
      })
      .catch((err) => {})
      .finally(() => {});

    return () => cancel();
  }, []);

  const onFinish = (values: any) => {
    let service = null;

    if (id) {
      service = shipService.update({
        id,
        ...values,
      });
    } else {
      service = shipService.add({
        ...values,
      });
    }

    const { request, cancel } = service;

    request
      .then(({}) => {
        navigate('/');
      })
      .catch((err) => {})
      .finally(() => {});
  };

  return (
    <>
      <Flex justify={'space-between'} align={'center'}>
        <Title level={2}>{id ? `Edit Ship` : 'Add Ship'}</Title>
        <div>
          <Button onClick={() => navigate('/')} type="primary">
            Return
          </Button>
        </div>
      </Flex>
      <Card bordered={false}>
        <Form
          form={form}
          name="basic"
          labelCol={{ span: 1 }}
          wrapperCol={{ span: 23 }}
          layout={'horizontal'}
          onFinish={onFinish}
          autoComplete="off"
        >
          <Form.Item<FieldType>
            label="Code"
            name="code"
            validateFirst
            required
            tooltip="Code Format: AAAA-1111-A1"
            rules={[
              {
                pattern: RegExp('^[A-Za-z]{4}-[0-9]{4}-[A-Za-z]{1}[0-9]{1}$'),
                message: 'Code must be of the format AAAA-1111-A1',
              },
              { required: true, message: 'Please input the code of the Ship!' },
            ]}
          >
            <MaskedInput mask={'aaaa-0000-a0'} />
          </Form.Item>

          <Form.Item<FieldType>
            label="Name"
            name="name"
            validateFirst
            rules={[
              { required: true, message: 'Please input the name of the Ship' },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item<FieldType>
            label="Length"
            name="length"
            rules={[
              {
                required: true,
                message: 'Please input the length of the Ship',
              },
              {
                type: 'number',
                min: 50,
                max: 500,
                message: 'Ship length must be between 50m and 500m',
              },
            ]}
          >
            <InputNumber />
          </Form.Item>

          <Form.Item<FieldType>
            label="Width"
            name="width"
            rules={[
              { required: true, message: 'Please input the width of the Ship' },
              {
                type: 'number',
                min: 10,
                max: 100,
                message: 'Ship width must be between 10m and 100m',
              },
            ]}
          >
            <InputNumber />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </>
  );
};

export default ShipForm;
