import 'antd/dist/antd.css'
// import './index.css'
import { notification } from 'antd'

notification.config({
    placement: 'bottomRight',
    bottom: 50,
    duration: 3,
    rtl: true,
  });

const notificationTypes = {
	ERROR: 'error',
    WARNING: 'warning',
    INFO: 'info',
    SUCCESS: 'success'
}

const getNotificationStyle = type => {
    return {
      success: {
        color: 'rgba(0, 0, 0, 0.65)',
        border: '1px solid #b7eb8f',
        backgroundColor: '#f6ffed'
      },
      warning: {
        color: 'rgba(0, 0, 0, 0.65)',
        border: '1px solid #ffe58f',
        backgroundColor: '#fffbe6'
      },
      error: {
        color: 'rgba(0, 0, 0, 0.65)',
        border: '1px solid #ffa39e',
        backgroundColor: '#fff1f0'
      },
      info: {
        color: 'rgba(0, 0, 0, 0.65)',
        border: '1px solid #91d5ff',
        backgroundColor: '#e6f7ff'
      }
    }[type]
  }

  const openCustomNotificationWithIcon = (type,title,desc) => {
    notification[type]({
      message: title,
      description:desc,
      style: getNotificationStyle(type)
    })
  }
  
  const WarningNotification = (desc) => openCustomNotificationWithIcon(notificationTypes.WARNING,"WARNING",desc);
  const InfoNotification = (desc) => openCustomNotificationWithIcon(notificationTypes.INFO,"INFO",desc);
  const ErrorNotification = (desc) => openCustomNotificationWithIcon(notificationTypes.ERROR,"ERROR",desc);
  const SuccessNotification = (desc) => openCustomNotificationWithIcon(notificationTypes.SUCCESS,"SUCCESS",desc);
 
  export default {
    WarningNotification,
    InfoNotification,
    ErrorNotification,
    SuccessNotification
  }