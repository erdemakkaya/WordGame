import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from "react-router-dom";
import { Input, Table, Tag, Typography, Button, Row, Col, Badge, Statistic, Modal, Tooltip } from 'antd';
import { ArrowUpOutlined, EditOutlined, DeleteOutlined, SearchOutlined, RedoOutlined} from '@ant-design/icons';
import WordLayout from '../Layout';
import WordService from '../../services/wordService'




export default function List() {

  async function fetchDeleteWord(id) {
    var response = await WordService.delete(id);
    if (response.success) {
      setAPIData((pre) => {
        return pre.filter((word) => word.id !== id);
      });
    }
  }

  const onDeleteWord = (id) => {
    Modal.confirm({
      title: "Are you sure, you want to delete this word record?",
      okText: "Yes",
      okType: "danger",
      onOk: () => {
        fetchDeleteWord(id);
      },
    });
  };

  const columns = [
    {
      title: 'Name',
      key: 'wordName',
      dataIndex: 'wordName',
      filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters }) => {

        return (
          <>
            <Input
              autoFocus
              value={selectedKeys[0]}
              placeholder="Type text here"
              onPressEnter={() => {
                confirm();

              }}
              onChange={(e) => {

                setSelectedKeys(e.target.value ? [e.target.value] : [])

              }
              }

              onBlur={() => {
                confirm();

              }}>

            </Input>

            <Tooltip title="search">
              <Button style={{ marginLeft: 'auto' }} type="primary" icon={<SearchOutlined />} onClick={() => {
                confirm();
              }} />
            </Tooltip>
            
            <Tooltip title="reset">
              <Button style={{ marginLeft: 'auto' }} type="danger"  icon={<RedoOutlined  />} onClick={() => {
                clearFilters();
              }} />
            </Tooltip>
          </>
        )
      },
      filterIcon: () => {
        return <SearchOutlined />
      },
      filterSearch: true,
      onFilter: (value, record) => record.wordName.toLowerCase().includes(value.toLowerCase()),
      render: text => <a>{text}</a>,
    },
    {
      title: 'Translation',
      key: 'turkishTranslator',
      dataIndex: 'wordName',
      filterSearch: true,
      onFilter: (value, record) => record.turkishTranslator.includes(value),
      dataIndex: 'turkishTranslator',
    },
    {
      title: 'Type',
      dataIndex: 'type',
    },

    {
      title: 'Tags',
      key: 'tags',
      dataIndex: 'tags',
      render: tags => (
        <>
          {tags.map(tag => {
            let color = tag.length > 5 ? 'geekblue' : 'green';
            if (tag === 'loser') {
              color = 'volcano';
            }
            return (
              <Tag color={color} key={tag}>
                {tag.toUpperCase()}
              </Tag>
            );
          })}
        </>
      ),
    },
    {
      title: 'Correcteness',
      dataIndex: 'Percentage',
      key: 'Percentage',
      sorter: (a, b) => (a.trueCount - a.falseCount) - (b.trueCount - b.falseCount),
      render: (item, row) => (
        <>
          {
            <Statistic
              title="Correcteness"
              value={(row.trueCount / (row.trueCount + row.falseCount)) * 100}
              precision={2}
              valueStyle={{ color: '#3f8600' }}
              prefix={<ArrowUpOutlined />}
              suffix="%"
            />

          }
        </>
      )
    },
    {
      title: 'AddedCount',
      dataIndex: 'addedCount',
      key: 'addedCount',
      sorter: (a, b) => (a.addedCount) - (b.trueCount - b.addedCount),
      render: counts => (
        <>
          {
            <Badge count={counts} />
          }
        </>
      )
    },
    {
      title: 'Action',
      dataIndex: 'id',
      key: 'action',
      render: (id) => {
        return (
          <>
            <Link to={`/create/${id}`}>

              <EditOutlined />
            </Link>

            <DeleteOutlined
              onClick={() => {
                onDeleteWord(id);
              }}
              style={{ color: "red", marginLeft: 12 }}
            />
          </>
        );
      }
    }
    // {
    //   title: 'Remember',
    //   dataIndex: 'remember',
    //   key: 'remember',
    //   render: remember => (
    //     <>
    //     {
    //         (remember) ? 
    //           (
    //             <Tag color="#00cc00">
    //               Active
    //             </Tag>
    //           ) :
    //           (
    //             <Tag color="#ff0000">
    //               Passive
    //             </Tag>
    //           ) 
    //       }
    //     </>
    //   ),
    // }
  ];
  const { Title } = Typography;
  const navigate = useNavigate()
  const [APIData, setAPIData] = useState([]);
  useEffect(() => {
    async function fetchMyAPI() {
      var response = await WordService.getAll();
      debugger;
      setAPIData(response.data.words);
    }
    fetchMyAPI()
  }, [])
  return (
    <WordLayout>
      <div>
        <Row gutter={[40, 0]}>
          <Col span={18}>
            <Title level={2}>
              User List
            </Title>
          </Col>
          <Col span={6}>
            <Button onClick={() => navigate('/create/0')} block>Add User</Button>
          </Col>
        </Row>
        <Row gutter={[40, 0]}>
          <Col span={24}>
            <Table
              rowKey='id'
              dataSource={APIData}
              columns={columns}
              expandable={{
                expandedRowRender: record => <p style={{ margin: 0 }}>{record.exampleSentence}</p>,
                rowExpandable: record => record.exampleSentence !== '',
              }}
            />
          </Col>
        </Row>
      </div>
    </WordLayout>
  )
}