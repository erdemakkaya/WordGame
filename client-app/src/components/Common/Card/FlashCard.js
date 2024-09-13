import React, { useState } from 'react';
import { Avatar, Card, List } from 'antd';
import './FlashCard.css';

const { Meta } = Card;

const Flashcard = ({ id, title, description, backTitle, backDescription,imageUrl, actions, isFirstCard, isFocused }) => {
  const [flipped, setFlipped] = useState(false);

  const flipCard = () => {
    setFlipped(!flipped);
  };

  return (
    <List.Item key={id}>
    <div className={`flashcard ${flipped ? 'flipped' : ''}`} onClick={flipCard}>
      <div className="flip-card-inner">
        <div className="flip-card-front">
          <Card
            id={id}
            style={{ width: 300 }}
            cover={
              <img
                alt="example"
                src={imageUrl}
                style={{ width: '100%', height: '199px', objectFit: 'cover' }} // Add this style
              />
            }
            actions={actions}
          >
            <Meta
              avatar={<Avatar src="https://api.dicebear.com/7.x/miniavs/svg?seed=8" />}
              title={title}
              description={description}
            />
          </Card>
        </div>
        <div className={`flip-card-back ${!isFirstCard && isFocused ? 'tour-overlay' : ''}`}>
          <Card
            id={id}
            style={{ width: 300 }}
            cover={
              <img
                alt="example"
                src={imageUrl}
                style={{ width: '100%', height: '199px', objectFit: 'cover' }} // Add this style
              />
            }
            actions={actions}
          >
            <Meta
              avatar={<Avatar src="https://api.dicebear.com/7.x/miniavs/svg?seed=8" />}
              title={backTitle}
              description={backDescription}
            />
          </Card>
        </div>
      </div>
    </div>
  </List.Item>
  );
};

export default Flashcard;
