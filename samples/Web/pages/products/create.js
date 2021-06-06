import { useRouter } from "next/router";

import { Row, Col, Button, Form, Input, InputNumber } from "antd";

import MainLayout from "../../layout/MainLayout";
import HeadDefault from "../../layout/head/HeadDefault";

const formItemLayout = {
  labelCol: {
    sm: {
      span: 4,
    },
  },
  wrapperCol: {
    sm: {
      span: 0,
    },
  },
};

const tailFormItemLayout = {
  wrapperCol: {
    sm: {
      span: 12,
      offset: 4,
    },
  },
};

const CreateProduct = () => {
  const router = useRouter();
  const [form] = Form.useForm();

  const onFinish = (values) => {
    console.log("Received values of form: ", values);
  };

  return (
    <>
      <HeadDefault
        title="Product Detail page | eCommerce"
        description="Product detail page of eCommerce."
      />
      <MainLayout>
        <Row>
          <Col span={12}>
            <Form
              {...formItemLayout}
              form={form}
              name="register"
              onFinish={onFinish}
              scrollToFirstError
            >
              <Form.Item
                label="Name"
                name="name"
                initialValue={"product 1"}
                rules={[
                  { required: true, message: "Please input product name" },
                ]}
              >
                <Input placeholder="name" />
              </Form.Item>

              <Form.Item
                label="Quantity"
                name="quantity"
                initialValue={10}
                rules={[{ required: true, message: "Please input quantity" }]}
              >
                <InputNumber placeholder="quantity" />
              </Form.Item>

              <Form.Item
                label="Cost"
                name="cost"
                initialValue={100}
                rules={[{ required: true, message: "Please input cost" }]}
              >
                <InputNumber placeholder="cost" />
              </Form.Item>

              <Form.Item
                label="Product Code"
                name="productCodeName"
                initialValue={"CODE"}
                rules={[
                  { required: true, message: "Please input product code" },
                ]}
              >
                <Input placeholder="productCodeName" />
              </Form.Item>

              <Form.Item {...tailFormItemLayout}>
                <Button type="primary" htmlType="submit">
                  Save
                </Button>
                <Button type="link" onClick={() => router.back()}>
                  Back
                </Button>
              </Form.Item>
            </Form>
          </Col>
        </Row>
      </MainLayout>
    </>
  );
};

export default CreateProduct;
