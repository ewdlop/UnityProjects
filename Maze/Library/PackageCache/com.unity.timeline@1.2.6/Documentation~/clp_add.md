import copy
import functools
import sys
import unittest
from test import test_support
from weakref import proxy
import pickle

@staticmethod
def PythonPartial(func, *args, **keywords):
    'Pure Python approximation of partial()'
    def newfunc(*fargs, **fkeywords):
        newkeywords = keywords.copy()
        newkeywords.update(fkeywords)
        return func(*(args + fargs), **newkeywords)
    newfunc.func = func
    newfunc.args = args
    newfunc.keywords = keywords
    return newfunc

def capture(*args, **kw):
    """capture all positional and keyword arguments"""
    return args, kw

def signature(part):
    """ return the signature of a partial object """
    return (part.func, part.args, part.keywords, part.__dict__)

class MyTuple(tuple):
    pass

class BadTuple(tuple):
    def __add__(self, other):
        return list(self) + list(other)

class MyDict(dict):
    pass

class TestPartial(unittest.TestCase):

    partial = functools.partial

    def test_basic_examples(self):
        p = self.partial(capture, 1, 2, a=10, b=20)
        self.assertEqual(p(3, 4, b=30, c=40),
                         ((1, 2, 3, 4), dict(a=10, b=30, c=40)))
        p = self.partial(map, lambda x: x*10)
        self.assertEqual(p([1,2,3,4]), [10, 20, 30, 40])

    def test_attributes(self):
        p = self.partial(capture, 1, 2, a=10, b=20)
        # attributes should be readable
        self.assertEqual(p.func, capture)
        self.assertEqual(p.args, (1, 2))
        self.assertEqual(p.keywords, dict(a=10, b=20))
        # attributes should not be writable
        self.assertRaises(TypeError, setattr, p, 'func', map)
        self.assertRaises(TypeError, setattr, p, 'args', (1, 2))
        self.assertRaises(TypeError, setattr, p, 'keywords', dict(a=1, b=2))

        p = self.partial(hex)
        try:
            del p.__dict__
        except TypeError:
            pass
        else:
            self.fail('partial object allowed __dict__ to be deleted')

    def test_argument_checking(s