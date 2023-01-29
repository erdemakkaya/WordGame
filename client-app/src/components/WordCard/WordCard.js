import React, { useEffect, useState } from 'react';
import {Row, Col,Typography, Card, Avatar,List,Button,message,Switch} from 'antd';
import { CloseOutlined, CheckOutlined,getTwoToneColor  } from '@ant-design/icons';
import WordLayout from '../Layout';
import WordNotification from '../Notification/WordNotification';
import WordService from '../../services/wordService'

const { Meta } = Card;
const {Title} = Typography;
function info(modalContext) {
  message.loading(modalContext,3);
}


const WordCard = (props)  => {
  const [array, setArray] = useState([]);
  const[language,setLanguage] = useState(false);
  
  function onChange(checked) {
    setLanguage(checked);
   }
  const onAnswer = async (answer,id) => {
    console.log(`answer ${answer} id ${id}`);
    var result;
    if(answer){
     result =await WordService.true(id);
    }
    else{
      result =await WordService.false(id);
    }
  debugger;
   if(result.success) {
     WordNotification.SuccessNotification("İşlem başarılı bir şekilde gerçekleşti.");
     setArray(array.filter(x=>x.id!=id))
   }
  }

    React.useEffect(() => {
      setArray(props.listofapi);
    }, [props.listofapi]);

    
    return (
      <WordLayout>
        <div>
        <Row gutter={[40, 0]}>
          <Col span={23}>
            <Title style={{textAlign: 'center'}} level={2}>
           Give your answer wisely WORD Count= { array.length}
            </Title>
            <Switch onChange={onChange} checkedChildren="TR" unCheckedChildren="EN" defaultChecked />
            </Col>
        </Row>
       
       <List
    grid={{ gutter: 12, column: 4 }}
    dataSource={array}
    renderItem={item => (
      <List.Item>
       <Card  loading={props.loading} size="small"
       extra={ <Button type="link" onClick={()=>{
         let modalContent= language? item.wordName:item.turkishTranslator;
         info(modalContent)
         
         }}>
       Translate
     </Button>
     
     }
    style={{ width:'100%', height: '75%' }}
    cover={
      <img
        alt="example"
        src="https://preply.com/wp-content/uploads/2018/04/word.jpg"
      />
    }
    actions={[
    // <Button>test</Button>,
    
      <CheckOutlined onClick={()=>onAnswer(true,item.id)}  twoToneColor={getTwoToneColor} theme="outlined" key="setting" style={{ fontSize: '16px', color: '#08c' }} />,
      <CloseOutlined onClick={()=>onAnswer(false,item.id)} key="edit"  style={{ fontSize: '16px', color: '#08c' }}/>,
    ]}
  >
    <Meta
      avatar={<Avatar src="https://joeschmoe.io/api/v1/random" />}
      title={language?item.turkishTranslator:item.wordName}
      description={language?'':item.exampleSentence}
    />
  </Card>
      </List.Item>
    )}
  />
        
        </div>
        </WordLayout>
      );

}

export default WordCard;