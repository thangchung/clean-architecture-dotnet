import React, { useCallback, useState } from "react";
import { useRouter } from "next/router";
import Link from "next/link";

import MainLayout from "../layout/MainLayout";
import HeadDefault from "../layout/head/HeadDefault";

import {
  Container,
  Card,
  CardHeader,
  CardBody,
  ButtonGroup,
  Button,
  UncontrolledButtonDropdown,
  DropdownMenu,
  DropdownItem,
  DropdownToggle,
} from "reactstrap";

import BootstrapTable from "react-bootstrap-table-next";
import paginationFactory from "react-bootstrap-table2-paginator";
import filterFactory, {
  textFilter,
  numberFilter,
  dateFilter,
} from "react-bootstrap-table2-filter";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import _ from "lodash";
import axios from "axios";
import moment from "moment";

const ApiUrl = "http://localhost:5002/api/products";

const Products = (props) => {
  const router = useRouter();
  const [page, setPage] = useState(1);
  const [sizePerPage, setSizePerPage] = useState(10);
  const [products, setProducts] = useState([]);
  const [totalSize, setTotalSize] = useState(0);
  const [selectedProduct, setSelectedProduct] = useState({});

  let nameFilter;
  let priceFilter;
  let quantityFilter;
  let createdFilter;

  const fetchData = useCallback(
    async (type, { page, sizePerPage, filters, sortField, sortOrder }) => {
      var queryModel = {
        sorts:
          sortOrder === "asc"
            ? [`${sortField}`]
            : [`${sortField}${_.startCase(_.toLower(sortOrder))}`],
        page: page,
        pageSize: sizePerPage,
      };

      var fts = [];

      for (const dataField in filters) {
        const { filterVal, filterType, comparator } = filters[dataField];
        if (filterType === "TEXT") {
          fts.push({
            fieldName: _.startCase(_.toLower(dataField)),
            comparision: "Contains",
            fieldValue: filterVal,
          });
        } else if (filterType === "NUMBER" && filterVal.number != null) {
          if (!filterVal.comparator == "") {
            fts.push({
              fieldName: _.startCase(_.toLower(dataField)),
              comparision:
                filterVal.comparator === "=" ? "==" : filterVal.comparator,
              fieldValue: filterVal.number,
            });
          }
        } else if (filterType === "DATE" && filterVal.date != null) {
          if (!(filterVal.comparator == "" || filterVal.comparator == null)) {
            fts.push({
              fieldName: _.startCase(_.toLower(dataField)),
              comparision:
                filterVal.comparator === "=" ? "==" : filterVal.comparator,
              fieldValue: filterVal.date,
            });
          }
        }
      }

      if (fts.length > 0) {
        queryModel = _.assign(queryModel, {
          filters: fts,
        });
      }

      axios.post(ApiUrl, queryModel).then((response) => {
        setProducts(response.data.items);
        setPage(response.data.page);
        setSizePerPage(response.data.pageSize);
        setTotalSize(response.data.totalItems);
      });
    },
    []
  );

  const rowSelect = (row, isSelect) => {
    setSelectedProduct(row);
  };

  const rowEdit = (cell, row, rowIndex, formatExtraData) => {
    return (
      <>
        <Link href={`product/${row.id}`}>
          <Button outline color="warning" size="sm">
            Edit
          </Button>
        </Link>
        &nbsp;
        <Link href={`product/${row.id}`}>
          <Button outline color="danger" size="sm">
            Delete
          </Button>
        </Link>
      </>
    );
  };

  const handleClearFilters = () => {
    if (nameFilter != undefined || _.isFunction(nameFilter)) {
      nameFilter("");
    }
    if (priceFilter != undefined || _.isFunction(priceFilter)) {
      priceFilter("");
    }
    if (quantityFilter != undefined || _.isFunction(quantityFilter)) {
      quantityFilter("");
    }
    if (createdFilter != undefined || _.isFunction(createdFilter)) {
      createdFilter();
    }
  };

  const selectRow = {
    mode: "checkbox",
    clickToSelect: true,
    bgColor: "#FFFFCC",
    onSelect: (row, isSelect, rowIndex, e) => {
      console.log(row.id);
      console.log(isSelect);
      console.log(rowIndex);
      console.log(e);
    },
    onSelectAll: (isSelect, rows, e) => {
      console.log(isSelect);
      console.log(rows);
      console.log(e);
    },
  };

  const columns = [
    {
      dataField: "id",
      text: "Product id",
      hidden: true,
    },
    {
      dataField: "name",
      text: "Product name",
      filter: textFilter({
        getFilter: (filter) => {
          nameFilter = filter;
        },
      }),
      sort: true,
    },
    {
      dataField: "cost",
      text: "Product price",
      filter: numberFilter({
        getFilter: (filter) => {
          priceFilter = filter;
        },
      }),
      sort: true,
    },
    {
      dataField: "quantity",
      text: "Quantity",
      filter: numberFilter({
        getFilter: (filter) => {
          quantityFilter = filter;
        },
      }),
      sort: true,
    },
    {
      dataField: "created",
      text: "Created",
      headerStyle: () => {
        return { width: "220px" };
      },
      formatter: (cell, row) => {
        return moment(cell).format("l");
      },
      filter: dateFilter({
        getFilter: (filter) => {
          createdFilter = filter;
        },
      }),
      sort: true,
    },
    {
      dataField: "productCodeName",
      text: "Code",
      headerStyle: () => {
        return { width: "80px", textAlign: "center" };
      },
    },
    {
      dataField: "actions",
      text: "",
      headerStyle: () => {
        return { width: "150px", textAlign: "center" };
      },
      formatter: rowEdit,
    },
  ];

  const defaultSorted = [
    {
      dataField: "name",
      order: "desc",
    },
  ];

  return (
    <>
      <HeadDefault
        title="Product Page | eCommerce"
        description="Product page of eCommerce."
      />
      <MainLayout>
        <h3>Product Page</h3>
        <Container fluid>
          <Card>
            <CardHeader tag="h3">
              <ButtonGroup>
                <UncontrolledButtonDropdown>
                  <DropdownToggle outline caret color="info">
                    Action
                  </DropdownToggle>
                  <DropdownMenu>
                    <DropdownItem header>
                      <a href="#">
                        <FontAwesomeIcon icon="trash" /> <b>Delete</b>{" "}
                      </a>
                    </DropdownItem>
                  </DropdownMenu>
                </UncontrolledButtonDropdown>
                <Button outline color="primary">
                  <FontAwesomeIcon icon="sync-alt" size="xs" /> <b>Refresh</b>
                </Button>
              </ButtonGroup>
              <ButtonGroup className="float-right">
                <Button outline color="secondary">
                  <FontAwesomeIcon icon="plus" /> <b>New</b>
                </Button>
                <Button outline color="primary" onClick={handleClearFilters}>
                  <FontAwesomeIcon icon="filter" /> <b>Filter</b>
                </Button>
                <UncontrolledButtonDropdown>
                  <DropdownToggle outline caret color="info"></DropdownToggle>
                  <DropdownMenu right>
                    <DropdownItem header>
                      <a href="#">
                        <FontAwesomeIcon icon="file-excel" /> <b>Export</b>{" "}
                      </a>
                    </DropdownItem>
                  </DropdownMenu>
                </UncontrolledButtonDropdown>
              </ButtonGroup>
            </CardHeader>
            <CardBody>
              <BootstrapTable
                bootstrap4
                hover
                remote
                keyField="id"
                data={products}
                columns={columns}
                defaultSorted={defaultSorted}
                filter={filterFactory()}
                pagination={paginationFactory({ page, sizePerPage, totalSize })}
                selectRow={selectRow}
                onTableChange={fetchData}
              />
            </CardBody>
          </Card>
        </Container>
      </MainLayout>
    </>
  );
};

export default Products;
