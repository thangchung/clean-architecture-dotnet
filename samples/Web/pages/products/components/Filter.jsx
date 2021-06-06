import React from "react";
import { Button, Row, Col, Form, Input, InputNumber } from "antd";

function Filter({ onFilterChange, onAdd, filter }) {
  var formRef = React.createRef();

  const handleSubmit = () => {
    const fields = formRef.current.getFieldsValue();
    onFilterChange(fields);
  };

  const handleReset = () => {
    const fields = formRef.current.getFieldsValue();
    for (let item in fields) {
      if ({}.hasOwnProperty.call(fields, item)) {
        if (fields[item] instanceof Array) {
          fields[item] = [];
        } else {
          fields[item] = undefined;
        }
      }
    }
    formRef.current.setFieldsValue(fields);
    handleSubmit();
  };

  return (
    <Form ref={formRef} name="control-ref">
      <Row gutter={24}>
        <Col md={{ span: 6 }}>
          <Form.Item name="name" label="Name" initialValue={filter['name']}>
            <Input placeholder={`Search...`} onSearch={handleSubmit} />
          </Form.Item>
        </Col>
        <Col md={{ span: 6 }}>
          <Form.Item name="quantity" label="Quantity" initialValue={filter['quantity']}>
            <InputNumber placeholder={`Search...`} onSearch={handleSubmit} style={{ width: '100%' }} />
          </Form.Item>
        </Col>
        <Col
          md={{ span: 12 }}
        >
          <Row type="flex" align="middle" justify="space-between">
            <div>
              <Button
                type="primary"
                htmlType="submit"
                className="margin-right"
                onClick={handleSubmit}
              >
                Search
              </Button>
              <Button onClick={handleReset}>Reset</Button>
            </div>
            <Button type="ghost" onClick={onAdd}>
              Create
            </Button>
          </Row>
        </Col>
      </Row>
    </Form>
  );
}

export default Filter;
