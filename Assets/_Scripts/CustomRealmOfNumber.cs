public class CustomRealmOfNumber {

    private const int MIN = 0;
    private const int MAX = 20;
    private int _value;

    public CustomRealmOfNumber(int value) { _value = value; }

    public int Value {
        get { return _value; }
        set { this._value = value; }
    }


    public void Add(int add) {
        _value = _value + add;
        if ( _value >= MAX ) {
            _value = _value - MAX;
        } else if ( _value < MIN ) {
            _value = _value + MAX;
        }
    }


//    public int Reflection() {
//        _value = _value + (MAX / 2);
//        if ( _value > MAX ) {
//            _value = _value - MAX;
//        } else if ( _value < MIN ) {
//            _value = _value + MAX;
//        }
//
//        return _value;
//    }
    
}
