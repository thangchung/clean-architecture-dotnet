import React from "react";
import Link from "next/link";
import { Table, Modal } from "antd";

import moment from "moment";

import DropOption from "./../../../components/DropOption/DropOption";

const { confirm } = Modal;

function List({
  onDeleteItem,
  onEditItem,
  onChange,
  rowSelection,
  products,
  pagination,
}) {
  const handleMenuClick = (record, e) => {
    if (e.key === "1") {
      onEditItem(record);
    } else if (e.key === "2") {
      confirm({
        title: `Are you sure delete this record?`,
        onOk() {
          onDeleteItem(record.id);
        },
      });
    }
  };

  const columns = [
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
      width: '30%',
      sorter: true,
      render: (text, record) => (
        <Link href={`products/${record.id}`}>{text}</Link>
      ),
    },
    {
      title: "Price",
      dataIndex: "cost",
      key: "cost",
    },
    {
      title: "Quantity",
      dataIndex: "quantity",
      key: "quantity",
    },
    {
      title: "Code",
      dataIndex: "productCodeName",
      key: "productCodeName",
    },
    {
      title: "Created",
      dataIndex: "created",
      key: "created",
      render: (text) => moment(text).format("l"),
    },
    {
      title: "Actions",
      key: "actions",
      width: '8%',
      fixed: "right",
      render: (_, record) => {
        return (
          <DropOption
            onMenuClick={(e) => handleMenuClick(record, e)}
            menuOptions={[
              { key: "1", name: `Update` },
              { key: "2", name: `Delete` },
            ]}
          />
        );
      },
    },
  ];

  return (
    <Table
      dataSource={products}
      columns={columns}
      pagination={pagination}
      rowKey={(record) => record.id}
      onChange={onChange}
      onDeleteItem={onDeleteItem}
      onEditItem={onEditItem}
      rowSelection={rowSelection}
    ></Table>
  );
}

export default List;
