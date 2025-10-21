import {useState} from "react";

const FormContact = (props) => {
    
    const [contactName, setContactName] = useState('');
    const [contactEmail, setContactEmail] = useState('');
    const [contactPhone, setContactPhone] = useState('');
    
    const sumbit = () => {
        if (!validateContact(contactName, contactEmail)) {
            return;
        } 
        props.addContact(contactName, contactPhone, contactEmail);
        setContactName('');
        setContactEmail('');
        setContactPhone('');
    }
    
    const validateContact = (contactName, contactEmail) => {
        if (contactName.length === 0 || contactEmail.length === 0 || contactPhone.length === 0) {
            return false;
        }
        return true;
    }
    
    return (
        <div>
            
            <div className="mb-3">
                <form>
                    <div className="mb-3">
                        <label className="form-label">Введите имя</label>
                        <input className="form-control" 
                               type="text"
                               value={contactName}
                               onChange={(e) => setContactName(e.target.value)}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Введите номер телефона</label>
                        <input className="form-control" 
                               type="text"
                               value={contactPhone}
                               onChange={(e) => setContactPhone(e.target.value)}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Введите e-mail</label>
                        <input className="form-control" 
                               type="text"
                               value={contactEmail}
                               onChange={(e) => {setContactEmail(e.target.value)}}
                        />
                    </div>
                </form>
            </div>
            
            <div>
                <button className="btn btn-primary"
                        onClick={() => {sumbit()}}
                >
                    Добавить контакт
                </button>
            </div>
        </div>
    );
}

export default FormContact;