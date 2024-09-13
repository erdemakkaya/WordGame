import React, { useState, useEffect } from 'react';
import { Avatar, Card, Row, Col, Typography, List, message, Switch, Modal, Statistic, Button } from 'antd';
import { CheckOutlined, CloseOutlined } from '@ant-design/icons';
import WordLayout from '../Layout';
import WordNotification from '../Notification/WordNotification';
import WordService from '../../services/wordService'
import Flashcard from '../Common/Card/FlashCard';
const { Title } = Typography;

const WordCard = (props) => {
  const [array, setArray] = useState([]);
  const [language, setLanguage] = useState(false);
  const [correctCount, setCorrectCount] = useState(0);
  const [incorrectCount, setIncorrectCount] = useState(0);
  const [currentCardIndex, setCurrentCardIndex] = useState(0);

  function onChange(checked) {
    setLanguage(checked);
  }

  const onAnswer = (answer, id) => {
    Modal.confirm({
      title: 'Do you want to submit your answer?',
      onOk: async () => {
        var result;
        if (answer) {
          result = await WordService.true(id);
          setCorrectCount(correctCount + 1); // Increment the correct count
        } else {
          result = await WordService.false(id);
          setIncorrectCount(incorrectCount + 1); // Increment the incorrect count
        }
        if (result.success) {
          setArray(array.filter(x => x.id !== id));
          Modal.success({
            content: 'Your answer has been submitted.',
          });
          // Move to the next card after submission
          setCurrentCardIndex(currentCardIndex + 1);
        }
      },
    });
  }

  useEffect(() => {
    setArray(props.listofapi);
  }, [props.listofapi]);

  return (
    <WordLayout>
      <Row gutter={[40, 0]}>
        <Col span={23}>
          <Title style={{ textAlign: 'center' }} level={2}>
            Give your answer wisely
          </Title>
          <Card title="Scoreboard" style={{ width: '100%', margin: '20px auto' }}>
            <Row gutter={16}>
              <Col span={8}>
                <Card bordered={false}>
                  <Statistic
                    title="Word Count"
                    value={array.length}
                  />
                </Card>
              </Col>
              <Col span={8}>
                <Card bordered={false}>
                  <Statistic
                    title="Correct answers"
                    value={correctCount}
                    valueStyle={{ color: '#3f8600' }}
                    prefix={<CheckOutlined />}
                  />
                </Card>
              </Col>
              <Col span={8}>
                <Card bordered={false}>
                  <Statistic
                    title="Incorrect answers"
                    value={incorrectCount}
                    valueStyle={{ color: '#cf1322' }}
                    prefix={<CloseOutlined />}
                  />
                </Card>
              </Col>
            </Row>
          </Card>
          <Switch onChange={onChange} checkedChildren="TR" unCheckedChildren="EN" defaultChecked />
          <List
            grid={{ gutter: 16, column: 4 }}
            dataSource={array}
            renderItem={(item, index) => (
              <Flashcard
                key={item.id}
                id={`card${item.id}`}
                title={item.wordName}
                description={item.exampleSentence}
                backTitle={item.turkishTranslator}
                backDescription={item.exampleSentence}
                imageUrl={item.imageUrl}
                actions={[
                  <CheckOutlined key="correct" onClick={() => onAnswer(true, item.id)} />,
                  <CloseOutlined key="false" onClick={() => onAnswer(false, item.id)} />,
                ]}
                isFirstCard={index === 0}
                isFocused={index === currentCardIndex} // Add a flag to indicate if the card is currently focused
              />
            )}
          />
        </Col>
      </Row>
    </WordLayout>
  );
}

export default WordCard;
